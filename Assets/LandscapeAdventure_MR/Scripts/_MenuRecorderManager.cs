using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;

public class _MenuRecorderManager : Singleton<_MenuRecorderManager> {

    [Range(0.1f, 0.2f)]
    public float noteDistance = 0.15f;

    public GameObject ParentPrefab;
    public GameObject MainMenuPrefab;
    // private GameObject MainMenuPrefabClone;

    private GameObject FocusedObject = null;

    private GazeStabilizer gazeStabilizer;

    private List<GameObject> menuList = new List<GameObject>();

    private Ray currentRay;
    private RaycastHit hitInfo;
    private float maxGazeDistance = 4.0f;
    private bool isAddNoteModeEnable = false;
    private bool Hit;

	// Use this for initialization
	void Start () {
		
        if (MainMenuPrefab == null || ParentPrefab == null)
        {
            Debug.Log("The prefab wasn't assigned in " + gameObject.name + ".");
        }

        MainMenuPrefab.SetActive(false);

        gazeStabilizer = this.gameObject.AddComponent<GazeStabilizer>();
	}
	
	// Update is called once per frame
	void Update () {

        if (MainMenuPrefab == null || ParentPrefab == null) { return; }

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
            GameObject MainMenuPrefabClone = Instantiate(MainMenuPrefab);
            MainMenuPrefabClone.transform.parent = ParentPrefab.transform;
            MainMenuPrefabClone.transform.position = currentRay.GetPoint(noteDistance);
            MainMenuPrefabClone.SetActive(true);
            menuList.Add(MainMenuPrefabClone);
        }
    }

    public void ShowAllNotes()
    {
        foreach (GameObject menu in menuList)
        {
            MeshRenderer renderer = menu.GetComponent<MeshRenderer>();
            renderer.enabled = true;
        }
    }

    public void HideAllNotes()
    {
        foreach (GameObject menu in menuList)
        {
            MeshRenderer renderer = menu.GetComponent<MeshRenderer>();
            renderer.enabled = false;
        }
    }

    public void DestroyNoteCommand()
    {
        Destroy(FocusedObject.transform.parent.transform.parent.gameObject);   
    }
}
