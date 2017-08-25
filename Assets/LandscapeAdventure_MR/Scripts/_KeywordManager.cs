using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows.Speech;
using HoloToolkit.Unity;

public class _KeywordManager : Singleton<_KeywordManager> {

    [Serializable]
    public struct KeysAndValues
    {
        public string Key;
        public UnityEvent Event;
    }

    public KeysAndValues[] KeysAndActions;

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, UnityEvent> dictionary = new Dictionary<string, UnityEvent>();

	// Use this for initialization
	void Start () {

        if (KeysAndActions.Length == 0)
        {
            Debug.Log("Anyone keyword wasn't assigned in " + gameObject.name + ".");
            return;
        }

        dictionary = KeysAndActions.ToDictionary(keysAndActions => keysAndActions.Key,
                                                 keysAndActions => keysAndActions.Event);

        keywordRecognizer = new KeywordRecognizer(dictionary.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        UnityEvent events;
        if (dictionary.TryGetValue(args.text, out events))
        {
            events.Invoke();
        }
    }

    public void StartRecognizer()
    {
        if (keywordRecognizer != null && !keywordRecognizer.IsRunning)
        {
            keywordRecognizer.Start();
        }
    }

    public void StopRecognizer()
    {
        if (keywordRecognizer != null && keywordRecognizer.IsRunning)
        {
            keywordRecognizer.Stop();
        }      
    }
}
