using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;

public class _MenuRecorderManager : Singleton<_MenuRecorderManager> {

    [Range(0.05f, 0.25f)]
    public float noteDistance = 0.15f;

    public GameObject ParentPrefab;
    public GameObject MenuRecorderPrefab;  

    public GameObject FocusedObject { get; private set; }

    private GazeStabilizer gazeStabilizer;

    private List<GameObject> menuRecorderPrefabsList = new List<GameObject>();

    private Ray currentRay;
    private RaycastHit hitInfo;
    private float maxGazeDistance = 4.0f;
    private bool Hit;

	// Use this for initialization
	void Start () {
		
        if (MenuRecorderPrefab == null || ParentPrefab == null)
        {
            Debug.Log("The prefab(-s) wasn't / weren't assigned in " + gameObject.name + ".");
        }
        MenuRecorderPrefab.SetActive(false);
        gazeStabilizer = this.gameObject.AddComponent<GazeStabilizer>();
        FocusedObject = null;
    }
	
	// Update is called once per frame
	void Update () {

        if (MenuRecorderPrefab == null || ParentPrefab == null) { return; }

        Vector3 gazeOrigin = Camera.main.transform.position;
        Vector3 gazeDirection = Camera.main.transform.forward;

        gazeStabilizer.UpdateStability(gazeOrigin, Camera.main.transform.rotation);
        gazeOrigin = gazeStabilizer.StablePosition;

        Hit = Physics.Raycast(gazeOrigin, gazeDirection, out hitInfo, maxGazeDistance);

        if (Hit)
        {
            currentRay = new Ray(hitInfo.point, Camera.main.transform.position - hitInfo.point);

            if (hitInfo.collider != null)
            {
                FocusedObject = hitInfo.collider.gameObject;
            }
            else
            {
                FocusedObject = null;
            }
        }
        else
        {
            FocusedObject = null;
        }
    }

    // "Add new note" voice command
    public void AddNewNoteFunction()
    {
        if (Hit)
        {
            GameObject MenuRecorderPrefabClone = Instantiate(MenuRecorderPrefab);           
            MenuRecorderPrefabClone.transform.position = currentRay.GetPoint(noteDistance);
            MenuRecorderPrefabClone.transform.parent = ParentPrefab.transform;
            MenuRecorderPrefabClone.SetActive(true);
            menuRecorderPrefabsList.Add(MenuRecorderPrefabClone);
        }
    }

    public void ShowAllNotes()
    {
        foreach (GameObject menu in menuRecorderPrefabsList)
        {
            menu.SetActive(false);
        }
    }

    public void HideAllNotes()
    {
        foreach (GameObject menu in menuRecorderPrefabsList)
        {
            menu.SetActive(true);
        }
    }

    public void DestroyNoteCommand()
    {
        Destroy(FocusedObject.transform.parent.transform.parent.gameObject);   
    }
}
