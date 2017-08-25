using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _InteractibleAction : MonoBehaviour {

    public GameObject ParentPrefab;
    public GameObject RotatePrefab;
    private GameObject rotatePrefab;

    private Quaternion defaultRotation = Quaternion.identity;

    // Use this for initialization
    void Start () {

        if (ParentPrefab == null || RotatePrefab == null)
        {
            Debug.Log("Check GameObject's in the Inspector panel for " + gameObject.name + ".");
        }

        defaultRotation = RotatePrefab.transform.rotation;

        rotatePrefab = InstantiatePrefab(RotatePrefab);
    }

    void Update()
    {
        Vector3 direction = Camera.main.transform.position - rotatePrefab.transform.position;
        Quaternion toQuat = Quaternion.LookRotation(-1 * direction);
        toQuat.z = 0;
        rotatePrefab.transform.rotation = toQuat * defaultRotation;
    }

    private GameObject InstantiatePrefab(GameObject original)
    {
        GameObject originalClone = Instantiate(original);
        originalClone.transform.parent = ParentPrefab.transform;
        originalClone.SetActive(false);

        return originalClone;
    }

    void GazeEntered()
    {
        rotatePrefab.SetActive(true);
    }

    void GazeExited()
    {
        rotatePrefab.SetActive(false);
    }
}
