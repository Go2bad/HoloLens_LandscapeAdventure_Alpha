using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class _ButtonCommands : MonoBehaviour {

    public UnityEvent[] Events;

    void Awake()
    {
        if (Events.Length == 0)
        {
            Debug.Log("The action wasn't assigned to " + gameObject.name + ".");
        }
    }

    void OnSelect()
    {
        foreach (UnityEvent unityEvent in Events)
        {
            unityEvent.Invoke();
        }
    }
}
