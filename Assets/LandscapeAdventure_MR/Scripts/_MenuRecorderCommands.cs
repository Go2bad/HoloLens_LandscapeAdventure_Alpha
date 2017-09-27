using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;
using HoloToolkit.Unity;

public class _MenuRecorderCommands : Singleton<_MenuRecorderCommands> {

    private DictationRecognizer dictationRecognizer;
    private StringBuilder textSoFar;
    private AudioSource audioSource;

    // private _KeywordManager keywordManager;

    public GameObject MainMenuPartPrefab;
    public GameObject LaterMenuPartPrefab;

    // Recognition of the user's speech.
    public TextMesh UserText;
    public TextMesh BriefDescriptionText;
    public TextMesh UserGuideText;

    private string micname = string.Empty;
    private int maxFreq;
    private int minFreq;

    private bool IsRecording = false;
    private bool IsPlaying = false;
    public bool isEdited { get; private set; }
    private bool isEditedFast = false;

    int tapCount = 0;

    new void Awake()
    {
        dictationRecognizer = new DictationRecognizer();

        dictationRecognizer.DictationHypothesis += DictationRecognizer_DictationHypothesis;
        dictationRecognizer.DictationResult += DictationRecognizer_DictationResult;
        dictationRecognizer.DictationComplete += DictationRecognizer_DictationComplete;
        dictationRecognizer.DictationError += DictationRecognizer_DictationError;

        Microphone.GetDeviceCaps(micname, out minFreq, out maxFreq);
        textSoFar = new StringBuilder();
    }

    // Use this for initialization
    void Start() {

        if (MainMenuPartPrefab == null || LaterMenuPartPrefab == null || UserText == null || BriefDescriptionText == null || UserGuideText == null)
        {
            Debug.Log("The prefab(-s) wasn't / weren't assigned in " + gameObject.name + ".");
        }

        isEdited = false;
        MainMenuPartPrefab.SetActive(false);

        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.spatialize = true;
        audioSource.spatialBlend = 1.0f;
        audioSource.dopplerLevel = 0.0f;
        audioSource.playOnAwake = false;
	}


    private void DictationRecognizer_DictationError(string error, int hresult)
    {
        UserText.text = "Error:" + "\n" + hresult;
    }

    private void DictationRecognizer_DictationComplete(DictationCompletionCause cause)
    {
        if (cause == DictationCompletionCause.TimeoutExceeded)
        {
            Microphone.End(micname);
        }

        if (cause == DictationCompletionCause.NetworkFailure)
        {
            Microphone.End(micname);
            UserText.text = "Internet connection was lost. Try again in a little bit";
        }

        if (cause == DictationCompletionCause.Complete)
        {
            Microphone.End(micname);
        }
    }

    private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence)
    {
        textSoFar.Append(text + ".");

        UserText.text = textSoFar.ToString();
    }

    private void DictationRecognizer_DictationHypothesis(string text)
    {
        UserText.text = textSoFar.ToString() + " " + text + "...";
    }


    // Update is called once per frame
    void Update()
    {
        if (!Microphone.IsRecording(micname) && IsRecording && dictationRecognizer.Status == SpeechSystemStatus.Running)
        {
            StopRecording();
        }

        if (!audioSource.isPlaying && IsPlaying == true)
        {
            StopPlaying();
        }

        if (isEditedFast)
        {
            StartCoroutine(EditNoteRoutine());
            isEditedFast = false;
        }
    }

    private IEnumerator EditNoteRoutine()
    {
        isEdited = false;
        yield return new WaitForSeconds(2.0f);
        MainMenuPartPrefab.SetActive(true);
        yield return null;
    }

    public void RecordMessage()
    {
        PhraseRecognitionSystem.Shutdown();

        dictationRecognizer.Start();
        audioSource.clip = Microphone.Start(micname, false, 15, maxFreq);
        IsRecording = true;


        textSoFar = null;
        textSoFar = new StringBuilder();
        UserText.text = "Dictation is starting. It may take time to display" + "\nyour text at first time but begin speaking now...";
    }

    public void StopRecording()
    {
        if (IsRecording)
        {
            if (dictationRecognizer.Status == SpeechSystemStatus.Running)
            {
                dictationRecognizer.Stop();
            }

            Microphone.End(micname);
            UserGuideText.text = "Tap on \"play message\" button to play your recording";
            // UserText.text = "Completed!";
            IsRecording = false;

            StartCoroutine(RestartSpeechSystem());
            
        }
    }

    private IEnumerator RestartSpeechSystem()
    {
        while (dictationRecognizer != null && dictationRecognizer.Status == SpeechSystemStatus.Running)
        {
            yield return null;
        }

        // keywordManager.StartRecognizer();
    }

    public void PlayMessage()
    {
        if (!IsRecording)
        {
            audioSource.Play();
            UserGuideText.text = "Tap on \"stop playing\" button to stop message";
            IsPlaying = true;
        }       
    }

    public void StopPlaying()
    {
        if (!IsRecording)
        {
            audioSource.Stop();
            UserGuideText.text = "Tap on \"play message\" button to play your recording again" + "\n" + "or tap on \"record\" button to rewrite your recording";
            IsPlaying = false;
        }
    } 

    public void OpenMainMenu()
    {
        tapCount++;

        if (tapCount == 1)
        {
            MainMenuPartPrefab.SetActive(true);
        }
    }

    public void DestroyNoteCommand()
    {
        Destroy(this.gameObject);
    }

    public void SaveNoteCommand()
    {
        MainMenuPartPrefab.SetActive(false);
        isEdited = true;

        int maxCount = 40;

        string fullText = Convert.ToString(UserText);
        fullText = fullText.Substring(0, maxCount);

        string textOne = fullText.Substring(0, 10);
        string textTwo = fullText.Substring(10, 20);
        string textThree = fullText.Substring(20, 30);
        string textFour = fullText.Substring(30, 40);
        textFour = textFour.Remove(fullText.LastIndexOf(' '));

        string helper = "\n";
        
        BriefDescriptionText.text = textOne + helper
                                   + textTwo + helper
                                   + textThree + helper
                                   + textFour + " ...";
    }

    public void EditNote()
    {               
        isEditedFast = true;
    }
}
