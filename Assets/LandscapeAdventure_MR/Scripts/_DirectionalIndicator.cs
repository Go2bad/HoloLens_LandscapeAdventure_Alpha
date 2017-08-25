using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _DirectionalIndicator : MonoBehaviour {

    public GameObject ParentPrefab;
    public GameObject DirectionalIndicator;
    MeshRenderer DirectionalIndicatorRenderer;
    private Quaternion DirectionalindicatorRotation = Quaternion.identity;
    private float showedFactor = 0.2f;
    private float distanceFromCursor = 0.25f;
    private bool IsDirectionalIndicatorVisible;

	// Use this for initialization
	void Start () {

        if (ParentPrefab == null || DirectionalIndicator == null)
        {
            Debug.Log("Check an assignment for " + gameObject.name + ". One of two prefabs can be unassigned");
        }

        DirectionalIndicator = Instantiate(DirectionalIndicator);

        DirectionalIndicatorRenderer = DirectionalIndicator.GetComponent<MeshRenderer>();
        if (DirectionalIndicatorRenderer == null)
        {
            DirectionalIndicatorRenderer = DirectionalIndicator.AddComponent<MeshRenderer>();
        }
        foreach (Rigidbody rigidbody in DirectionalIndicator.GetComponents<Rigidbody>())
        {
            Destroy(rigidbody);
        }
        foreach (Collider collider in DirectionalIndicator.GetComponents<Collider>())
        {
            Destroy(collider);
        }

        DirectionalIndicator.transform.SetParent(gameObject.transform);

        DirectionalindicatorRotation = DirectionalIndicator.transform.rotation;
        DirectionalIndicatorRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (ParentPrefab == null || DirectionalIndicator == null)
        {
            return;
        }

        Vector3 headToObjectDirection = (gameObject.transform.position - Camera.main.transform.position).normalized;
        Vector3 DirectionalIndicatorForward = (Vector3.ProjectOnPlane(headToObjectDirection, -1 * Camera.main.transform.forward)).normalized;

        IsDirectionalIndicatorVisible = !IsTargetVisible();
        DirectionalIndicatorRenderer.enabled = IsDirectionalIndicatorVisible;

        if (IsDirectionalIndicatorVisible)
        {
            Vector3 Position;
            Quaternion Rotation;

            Vector3 Origin = ParentPrefab.transform.position;
            

            if (DirectionalIndicatorForward == Vector3.zero)
            {
                DirectionalIndicatorForward = Camera.main.transform.right;
            }

            Position = Origin + DirectionalIndicatorForward * distanceFromCursor;
            Rotation = Quaternion.LookRotation(Camera.main.transform.forward, headToObjectDirection) * DirectionalindicatorRotation;

            DirectionalIndicator.transform.position = Position;
            DirectionalIndicator.transform.rotation = Rotation;
        }

	}

    private bool IsTargetVisible()
    {
        Vector3 directionalIndicatorVector = Camera.main.WorldToViewportPoint(this.transform.parent.position);

        return (directionalIndicatorVector.x > showedFactor && directionalIndicatorVector.x < 1 - showedFactor &&
                directionalIndicatorVector.y > showedFactor && directionalIndicatorVector.y < 1 - showedFactor &&
                directionalIndicatorVector.z > 0);
    }
}
