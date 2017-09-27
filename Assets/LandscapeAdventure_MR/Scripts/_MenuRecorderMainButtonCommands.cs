using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class _MenuRecorderMainButtonCommands : Singleton<_MenuRecorderMainButtonCommands> {

    public GameObject LaterMenuPartPrefab;
    private GameObject focusedObject = null;

    private _MenuRecorderCommands menuRecorderCommands;


    void GazeEntered()
    {
        focusedObject = _MenuRecorderManager.Instance.FocusedObject;
        menuRecorderCommands = focusedObject.transform.parent.gameObject.GetComponent<_MenuRecorderCommands>();

        if (menuRecorderCommands.isEdited)
        {
            LaterMenuPartPrefab.SetActive(true);
        }
    }

    void GazeExited()
    {
        StartCoroutine(GazeExitedAlgorithm());
    }

    private IEnumerator GazeExitedAlgorithm()
    {
        yield return new WaitForSeconds(2.0f);
        LaterMenuPartPrefab.SetActive(false);
        yield return null;
    }
}
