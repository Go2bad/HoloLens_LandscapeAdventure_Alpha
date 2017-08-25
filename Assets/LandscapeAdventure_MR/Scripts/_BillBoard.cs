using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PivotAxis
{
    Free,
    X,
    Y
}

public class _BillBoard : MonoBehaviour {

    public PivotAxis AxisToRotate = PivotAxis.Free;

    Quaternion DefaultRotation = Quaternion.identity;

	// Use this for initialization
	void Start () {
        DefaultRotation = this.gameObject.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 direction = Camera.main.transform.position - this.gameObject.transform.position;

        switch (AxisToRotate)
        {
            case PivotAxis.X:
                direction.x = this.gameObject.transform.position.x;
                break;
            case PivotAxis.Y:
                direction.y = this.gameObject.transform.position.y;
                break;
            case PivotAxis.Free:
            default:
                break;
        }

        this.gameObject.transform.rotation = Quaternion.LookRotation(-1 * direction) * DefaultRotation;
    }
}
