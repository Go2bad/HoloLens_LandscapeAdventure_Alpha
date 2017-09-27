using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class _LandscapeCommands : Singleton<_LandscapeCommands> {

    public GameObject InformationPrefab;
    public GameObject MeshId24Prefab;
    private Interpolator interpolator;

    public bool Hit { get; private set; }

    private _Tagalong tagaLong;
    private _BillBoard billBoard;
    public RaycastHit HitInfo { get; private set; }
    private RaycastHit hitInfo;
    private bool IsPlacingMode = false;
    private bool IsInformationEnable = true;
    private float maxGazeDistance = 4.0f;
    private float lastDistance = 4.0f;

    private int layerNumber = 31;
    public int layerNumberConverted { get; private set; }

    private Quaternion defaultRotation = Quaternion.identity;
    private Vector3 defaultScale = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        if (MeshId24Prefab == null || InformationPrefab)
        {
            Debug.Log("The prefab(-s) wasn't / weren't assigned in " + gameObject.name + ".");
        }

        Quaternion toQuat = Quaternion.identity;

        interpolator = this.gameObject.AddComponent<Interpolator>();

        interpolator.SmoothLerpToTarget = true;
        interpolator.SmoothScaleLerpRatio = 0.1f;
        interpolator.SmoothRotationLerpRatio = 0.1f;

        interpolator.RotationDegreesPerSecond = 20.0f;
        interpolator.ScalePerSecond = 20.0f;

        layerNumberConverted = 1 << layerNumber;

        billBoard = this.gameObject.GetComponent<_BillBoard>();
        tagaLong = MeshId24Prefab.gameObject.GetComponent<_Tagalong>();
        defaultRotation = this.gameObject.transform.rotation;
        defaultScale = this.gameObject.transform.localScale;
    }

    
	public void TappedCommand()
    {
        ///
    }
    

    public void FollowMeCommand()
    {
        if (!IsPlacingMode)
        {
            billBoard.enabled = true;
            tagaLong.enabled = true;
        }
    }

    public void StopFollowMeCommand()
    {
        if (!IsPlacingMode)
        {
            billBoard.enabled = false;
            tagaLong.enabled = false;
        }
    }

    public void ShowOrHideInformation()
    {
        IsInformationEnable = !IsInformationEnable;

        if (IsInformationEnable)
        {
            InformationPrefab.SetActive(true);
        }
        else
        {
            InformationPrefab.SetActive(false);
        }
    }

    public void ResetModelCommand()
    {
        interpolator.SetTargetLocalRotation(defaultRotation);
        interpolator.SetTargetLocalScale(defaultScale);
    }
}
