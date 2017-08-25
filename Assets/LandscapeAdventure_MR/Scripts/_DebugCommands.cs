using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _DebugCommands : MonoBehaviour {

	void DebugLogs()
    {
        // Prefab assignment
        Debug.Log("The prefab(-s) wasn't / weren't assigned in " + gameObject.name + ".");
    }
}
