using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;

public class _GazeManager : Singleton<_GazeManager> {

    public bool IsIndicator = false;
    public bool Hit { get; private set; }
    public RaycastHit HitInfo { get; private set; }
    private float maxGazeDistance = 9.0f;

    private GazeStabilizer gazeStabilier;

    // Use this for initialization
    void Start()
    {
        gazeStabilier = this.gameObject.AddComponent<GazeStabilizer>();      
    }

	// Update is called once per frame
	void Update () {

        Vector3 gazeOrigin = Camera.main.transform.position;
        Vector3 gazeDirection = Camera.main.transform.forward;

        gazeStabilier.UpdateStability(gazeOrigin, Camera.main.transform.rotation);
        gazeOrigin = gazeStabilier.StablePosition;

        RaycastHit hitInfo;

        Hit = Physics.Raycast(gazeOrigin, gazeDirection, out hitInfo, maxGazeDistance);

        HitInfo = hitInfo;

        if (Hit)
        {
            if (IsIndicator)
            {
                this.gameObject.transform.position = gazeOrigin + (gazeDirection * maxGazeDistance);
                this.gameObject.transform.up = gazeDirection;
            }
            else
            {
                this.gameObject.transform.position = hitInfo.point;
                this.gameObject.transform.up = hitInfo.normal;
            }
        }
        else
        {
            this.gameObject.transform.position = gazeOrigin + (gazeDirection * maxGazeDistance);
            this.gameObject.transform.up = gazeDirection;
        }
	}
}
