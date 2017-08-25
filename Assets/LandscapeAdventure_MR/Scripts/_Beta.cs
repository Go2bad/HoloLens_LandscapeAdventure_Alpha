using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Beta : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Destroy(this.gameObject.transform.parent.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
