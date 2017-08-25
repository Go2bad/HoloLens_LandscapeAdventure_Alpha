using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class _LandscapeCommands : Singleton<_LandscapeCommands> {

    public GameObject InformationPrefab;
    public GameObject MeshId24;

    public bool Hit { get; private set; }

    private _Tagalong tagaLong;
    private _BillBoard billBoard;
    public RaycastHit HitInfo { get; private set; }
    private RaycastHit hitInfo;
    private bool IsPlacingMode = false;
    private bool IsInformationEnable = true;
    private float maxGazeDistance = 4.0f;

    private int layerNumber = 31;
    public int layerNumberConverted { get; private set; }

    Quaternion defaultRotation = Quaternion.identity;

    // Use this for initialization
    void Start()
    {
        if (MeshId24 == null || InformationPrefab)
        {
            Debug.Log("The prefab(-s) wasn't / weren't assigned in " + gameObject.name + ".");
        }

        layerNumberConverted = 1 << layerNumber;

        billBoard = this.gameObject.GetComponent<_BillBoard>();
        tagaLong = MeshId24.gameObject.GetComponent<_Tagalong>();
        defaultRotation = this.gameObject.transform.rotation;
    }

	public void TappedCommand()
    {
        IsPlacingMode = !IsPlacingMode;

        if (IsPlacingMode)
        {
            tagaLong.enabled = false;
            billBoard.enabled = true;

            //_SpatialMapping.Instance.DrawVisualMeshes = true;
        }
        else
        {
            billBoard.enabled = false;
            tagaLong.enabled = false;

            //_SpatialMapping.Instance.DrawVisualMeshes = false;
        }
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

    // Update is called once per frame
    void Update()
    {
        if (IsPlacingMode)
        {
            Vector3 gazeOrigin = Camera.main.transform.position;
            Vector3 gazeDirection = Camera.main.transform.forward;
            // Ray ray = new Ray(gazeOrigin, gazeDirection);

            Hit = Physics.Raycast(gazeOrigin, gazeDirection, out hitInfo, maxGazeDistance, layerNumberConverted);

            HitInfo = hitInfo;

            if (Hit)
            {
                this.gameObject.transform.position = hitInfo.point;
            }
            else
            {
                // this.gameObject.transform.position = ray.GetPoint(maxGazeDistance);
                this.gameObject.transform.position = gazeOrigin + (gazeDirection * maxGazeDistance);
            }
        }
    }
}
