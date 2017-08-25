using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class _Fitbox : MonoBehaviour {

    [Range(1.0f, 5.0f)]
    public float speedFactor = 3.0f;
    [Range(0.1f, 2.0f)]
    public float timeForUpdate = 1.0f;

    private float updateTime = 0.0f;
    private bool IsActive = true;

    public GameObject LandscapePrefab;
    public GameObject HandUpPrefab;
    private MeshRenderer handUpRenderer;

    private Interpolator interpolator;

	// Use this for initialization
	void Start () {

        if (LandscapePrefab == null || HandUpPrefab == null)
        {
            Debug.Log("The prefab(-s) wasn't / weren't assigned in " + gameObject.name + ".");
        }

        LandscapePrefab.SetActive(false);

        interpolator = this.GetComponent<Interpolator>();
        interpolator.PositionPerSecond = speedFactor;

        handUpRenderer = HandUpPrefab.GetComponent<MeshRenderer>();
	}

    void OnSelect()
    {
        LandscapePrefab.transform.position = gameObject.transform.position;
        LandscapePrefab.transform.rotation = gameObject.transform.rotation;
        LandscapePrefab.SetActive(true);

        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {

        Transform transform = Camera.main.transform;

        interpolator.SetTargetPosition(transform.position + (transform.forward * 2.0f));
        interpolator.SetTargetRotation(Quaternion.LookRotation(transform.forward, transform.up));

        if ((Time.time - updateTime) > timeForUpdate)
        {
            ColorChanger();
            updateTime = Time.time;
        }
	}

    private void ColorChanger()
    {
        IsActive =! IsActive;
        handUpRenderer.enabled = IsActive;
    }
}
