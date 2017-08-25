using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlacementSurfaces
{
    Horizontal = 1,
    Vertical = 2
}

public class Placeable : MonoBehaviour {

    public PlacementSurfaces PlacementSurface = PlacementSurfaces.Horizontal;

    public Material PlaceableShadow = null;
    public Material NotPlaceableShadow = null;
    public Material PlaceableBounds = null;
    public Material NotPlaceableBounds = null;

    private float hoverDistance = 0.2f;

    private BoxCollider boxCollider;

    // Use this for initialization
    void Start () {
		
	}
	
    void OnSelect()
    {

    }

	// Update is called once per frame
	void Update () {
		
	}
}