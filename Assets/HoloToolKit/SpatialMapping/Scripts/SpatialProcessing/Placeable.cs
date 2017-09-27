using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.SpatialMapping;
using System;

public enum PlacementSurfaces
{
    Horizontal = 1,
    Vertical = 2
}

public class Placeable : MonoBehaviour {

    public PlacementSurfaces PlacementSurface = PlacementSurfaces.Horizontal;

    public List<GameObject> ObjectsToHide;

    public Material PlaceableShadow = null;
    public Material NotPlaceableShadow = null;
    public Material PlaceableBounds = null;
    public Material NotPlaceableBounds = null;

    private float hoverDistance = 0.2f;
    private float speedFactor = 0.05f;
    private bool managingBoxCollider = false;
    private bool isPlacing = false;

    private GameObject boundsAsset = null;
    private GameObject shadowAsset = null;

    private BoxCollider boxCollider;

    // The object's position where it'll be placed
    private Vector3 targetPosition;

    private float lastDistance = 2.0f;

    // Use this for initialization
    void Start ()
    {
        ///
	}
	
    void OnSelect()
    {
        ///
    }

	// Update is called once per frame
	void Update ()
    {
       ///
	}
}