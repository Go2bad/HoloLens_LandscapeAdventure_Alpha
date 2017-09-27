using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ToolboxCommands : MonoBehaviour {

    public GameObject MainToolboxPartPrefab;

    private bool isExpandActive = false;

	// Use this for initialization
	void Start () {
		
        if (MainToolboxPartPrefab == null)
        {
            Debug.Log("The prefab(-s) wasn't / weren't assigned in " + gameObject.name + ".");
        }

        MainToolboxPartPrefab.SetActive(false);
    }

    public void ExpandMenu()
    {
        isExpandActive = !isExpandActive;

        if (isExpandActive)
        {
            MainToolboxPartPrefab.SetActive(true);
        }
        else
        {
            MainToolboxPartPrefab.SetActive(false);
        }
    }
}
