using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ToolboxCommands : MonoBehaviour {

    public GameObject ExpandMenuPrefab;

	// Use this for initialization
	void Start () {
		
        if (ExpandMenuPrefab == null)
        {
            Debug.Log("The prefab(-s) wasn't / weren't assigned in " + gameObject.name + ".");
        }

        ExpandMenuPrefab.SetActive(false);
    }

    void ExpandMenu()
    {
        ExpandMenuPrefab.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
