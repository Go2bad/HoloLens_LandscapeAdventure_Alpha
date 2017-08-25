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

    private _KeywordManager keywordManager;

    public TextMesh TextToDisplay;
    public TextMesh TextUser;
    public GameObject InteractionMenuPrefab;

    private string micname = string.Empty;
    private int maxFreq;
    private int minFreq;

    private bool IsRecording = false;
    private bool IsPlaying = false;

    // +
    private bool IsActive = false;

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

        if (TextUser == null || TextToDisplay == null || InteractionMenuPrefab == null)
        {
            Debug.Log("Check GameObject's in the Inspector panel for " + gameObject.name + ".");
        }

        keywordManager = this.gameObject.GetComponent<_KeywordManager>();

        InteractionMenuPrefab.SetActive(false);

        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.spatialize = true;
        audioSource.spatialBlend = 1.0f;
        audioSource.dopplerLevel = 0.0f;
        audioSource.playOnAwake = false;
	}


    private void DictationRecognizer_DictationError(string error, int hresult)
    {
        TextUser.text = "Error:" + "\n" + hresult;
    }

    private void DictationRecognizer_DictationComplete(DictationCompletionCause cause)
    {
        if (cause == DictationCompletionCause.TimeoutExceeded)
        {
            Microphone.End(micname);
            TextUser.text = "Dictation was completed";
        }

        if (cause == DictationCompletionCause.NetworkFailure)
        {
            Microphone.End(micname);
            TextUser.text = "Internet connection was lost. Try again in a little bit";
        }

        if (cause == DictationCompletionCause.Complete)
        {
            Microphone.End(micname);
            TextUser.text = "Dictation was completed";
        }
    }

    private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence)
    {
        textSoFar.Append(text + ".");

        TextUser.text = textSoFar.ToString();
    }

    private void DictationRecognizer_DictationHypothesis(string text)
    {
        TextUser.text = textSoFar.ToString() + " " + text + "...";
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
    }

    public void RecordMessage()
    {
        PhraseRecognitionSystem.Shutdown();

        dictationRecognizer.Start();
        audioSource.clip = Microphone.Start(micname, false, 15, maxFreq);
        IsRecording = true;


        textSoFar = null;
        textSoFar = new StringBuilder();
        TextUser.text = "Dictation is starting. It may take time to display" + "\nyour text at first time but begin speaking now...";
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
            TextToDisplay.text = "Tap on \"play message\" button to play your recording";
            TextUser.text = "Completed!";
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

        keywordManager.StartRecognizer();
    }

    public void PlayMessage()
    {
        if (!IsRecording)
        {
            audioSource.Play();
            TextToDisplay.text = "Tap on \"stop playing\" button to stop message";
            IsPlaying = true;
        }
        
    }

    public void StopPlaying()
    {
        if (!IsRecording)
        {
            audioSource.Stop();
            TextToDisplay.text = "Tap on \"play message\" button to play your recording again" + "\n" + "or tap on \"record\" button to rewrite your recording";
            IsPlaying = false;
        }
    } 

    public void OpenMainMenu()
    {
        IsActive = !IsActive;

        if (IsActive)
        {
            InteractionMenuPrefab.SetActive(true);
        }
        else
        {
            InteractionMenuPrefab.SetActive(false);
        }
    }

    public void SaveNoteCommand()
    {
        TextToDisplay.text = "Saved!";
    }

    public void DestroyMenu()
    {
        Destroy(this.gameObject);
    }
}
