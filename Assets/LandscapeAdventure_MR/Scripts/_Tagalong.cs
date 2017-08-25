using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class _Tagalong : Singleton<_Tagalong> {

    public GameObject Landscape;

    Collider collider;
    Vector3 ToPosition;
    Interpolator interpolator;
    RaycastHit hitInfo;
    Plane[] planes;

    private float smoothingFactor = 0.5f;
    public float maxDistanceFromUser = 1.25f;
    public float maxDistanceOfLandsacpeFromUser = 4.0f;
    private float speedFactor = 5.0f;

    public bool IsLandscape = true;

    private int frustumLeft = 0;
    private int frustumRight = 1;
    private int frustumDown = 2;
    private int frustumUp = 3;

    // Use this for initialization
    void Start () {

        if (Landscape == null)
        {
            Debug.Log("The object wasn't assigned in " + gameObject.name + ".");
        }

        if (IsLandscape == false)
        {
            interpolator = this.gameObject.AddComponent<Interpolator>();
        }
        else
        {
            interpolator = Landscape.gameObject.AddComponent<Interpolator>();
        }
        
        interpolator.SmoothLerpToTarget = true;
        interpolator.SmoothPositionLerpRatio = smoothingFactor;

        collider = this.gameObject.GetComponent<Collider>();

        if (collider == null)
        {
            collider = this.gameObject.AddComponent<Collider>();
        }
	}
	
	// Update is called once per frame
	void Update () {

        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if (IsLandscape == false)
        {
            if (CalulateDirectionFromPlanesToAABB(this.gameObject.transform.position, out ToPosition))
            {
                interpolator.PositionPerSecond = speedFactor;
                interpolator.SetTargetPosition(ToPosition);
            }
            else
            {
                Ray ray = new Ray(Camera.main.transform.position, this.gameObject.transform.position - Camera.main.transform.position);
                this.gameObject.transform.position = ray.GetPoint(maxDistanceFromUser);
            }
        }
        else
        {
            if (CalulateDirectionFromPlanesToAABB(this.gameObject.transform.parent.position, out ToPosition))
            {
                interpolator.PositionPerSecond = speedFactor;
                interpolator.SetTargetPosition(ToPosition);
            }
            else
            {
                Ray ray = new Ray(Camera.main.transform.position, this.gameObject.transform.parent.position - Camera.main.transform.position);
                this.gameObject.transform.parent.position = ray.GetPoint(maxDistanceOfLandsacpeFromUser);
            }
        }
    }

    private bool CalulateDirectionFromPlanesToAABB(Vector3 FromPosition, out Vector3 ToPosition)
    {
        bool needsToMove = !GeometryUtility.TestPlanesAABB(planes, collider.bounds);

        if (IsLandscape == false)
        {
            ToPosition = Camera.main.transform.position + Camera.main.transform.forward * maxDistanceFromUser;
        }
        else
        {
            ToPosition = Camera.main.transform.position + Camera.main.transform.forward * maxDistanceOfLandsacpeFromUser;
        }
        

        Plane plane = new Plane();
        Ray ray = new Ray(ToPosition, Vector3.zero);

        bool moveRight = planes[frustumLeft].GetDistanceToPoint(FromPosition) < 0.4;
        bool moveLeft = planes[frustumRight].GetDistanceToPoint(FromPosition) < 0.4;

        if (moveRight)
        {
            plane = planes[frustumLeft];
            ray.direction = -Camera.main.transform.right;
        }

        if (moveLeft)
        {
            plane = planes[frustumRight];
            ray.direction = Camera.main.transform.right;
        }

        bool moveUp = planes[frustumDown].GetDistanceToPoint(FromPosition) < 0.4;
        bool moveDown = planes[frustumUp].GetDistanceToPoint(FromPosition) < 0.4;

        if (moveUp)
        {
            plane = planes[frustumUp];
            ray.direction = -Camera.main.transform.up;
        }

        if (moveDown)
        {
            plane = planes[frustumDown];
            ray.direction = Camera.main.transform.up;
        }

        ray = new Ray(Camera.main.transform.position, ToPosition - Camera.main.transform.position);

        if (Landscape == false)
        {
            ToPosition = ray.GetPoint(maxDistanceFromUser);
        }
        else
        {
            ToPosition = ray.GetPoint(maxDistanceOfLandsacpeFromUser);
        }
        

        return needsToMove;
    }
}
