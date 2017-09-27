using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class _GestureAction : Singleton<_GestureAction> {

    private float perfomingFactor = 9.0f;
	
	// Update is called once per frame
	void Update () {
		
        if (_GestureManager.Instance.IsRotating)
        {
            float rotateFactor = _GestureManager.Instance.RotateRecognizerPosition.x * perfomingFactor;
            this.gameObject.transform.Rotate(new Vector3(0, -1 * rotateFactor, 0));
        }

        if (_GestureManager.Instance.IsScalling)
        {
            // float scaleFactor = _GestureManager.Instance.ScaleRecognizerPosition.x * 0.01f;
            // this.gameObject.transform.parent.localScale = (new Vector3(scaleFactor, scaleFactor, scaleFactor)).normalized;

            Vector3 direction = _GestureManager.Instance.ScaleRecognizerPosition;
            float zoomHandDistanceFactor = Mathf.Abs(Mathf.Pow(direction.x, 3)) * Mathf.Sign(direction.x);
            float newScale = this.gameObject.transform.localScale.x + (Time.deltaTime * zoomHandDistanceFactor * 0.8f);
            this.gameObject.transform.localScale = new Vector3(newScale, newScale, newScale);
        }
	}
}
