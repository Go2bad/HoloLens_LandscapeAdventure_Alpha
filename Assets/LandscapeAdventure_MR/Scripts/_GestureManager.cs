using System.Collections;
using System.Collections.Generic;
using UnityEngine.VR.WSA.Input;
using UnityEngine;
using HoloToolkit.Unity;
using System;

public class _GestureManager : Singleton<_GestureManager> {

    private RaycastHit hitInfo;
    private GameObject FocusedObject;
    private GameObject OldFocusedObject;

    public bool IsRotating { get; private set; }
    public Vector3 RotateRecognizerPosition { get; private set; }
    GestureRecognizer RotationRecognizer;

    public bool IsScalling { get; private set; }
    public Vector3 ScaleRecognizerPosition { get; private set; }
    GestureRecognizer ScaleRecognizer;

    GestureRecognizer ActiveRecognizer;

	// Use this for initialization
	void Start () {

        ScaleRecognizer = new GestureRecognizer();
        ScaleRecognizer.SetRecognizableGestures(GestureSettings.Tap | GestureSettings.NavigationX);

        ScaleRecognizer.TappedEvent += ScaleRecognizer_TappedEvent;
        ScaleRecognizer.NavigationCanceledEvent += ScaleRecognizer_NavigationCanceledEvent;
        ScaleRecognizer.NavigationCompletedEvent += ScaleRecognizer_NavigationCompletedEvent;
        ScaleRecognizer.NavigationUpdatedEvent += ScaleRecognizer_NavigationUpdatedEvent;
        ScaleRecognizer.NavigationStartedEvent += ScaleRecognizer_NavigationStartedEvent;

        RotationRecognizer = new GestureRecognizer();
        RotationRecognizer.SetRecognizableGestures(GestureSettings.Tap | GestureSettings.NavigationX);

        RotationRecognizer.TappedEvent += RotationRecognizer_TappedEvent;
        RotationRecognizer.NavigationCanceledEvent += RotationRecognizer_NavigationCanceledEvent;
        RotationRecognizer.NavigationCompletedEvent += RotationRecognizer_NavigationCompletedEvent;
        RotationRecognizer.NavigationUpdatedEvent += RotationRecognizer_NavigationUpdatedEvent;
        RotationRecognizer.NavigationStartedEvent += RotationRecognizer_NavigationStartedEvent;

        ChangeRecognizer(RotationRecognizer);

        FocusedObject = null;
        OldFocusedObject = null;
	}

    private void SendMessageObject()
    {
        // Rewrite based on "IntercatibleManager" script

        if (FocusedObject != null)
        {
            FocusedObject.SendMessageUpwards("OnSelect");
        }
    }

    void Update()
    {
        OldFocusedObject = FocusedObject;

        if (_GazeManager.Instance.Hit)
        {
            hitInfo = _GazeManager.Instance.HitInfo;

            if (hitInfo.collider != null)
            {
                FocusedObject = hitInfo.collider.gameObject;
            }
            else
            {
                FocusedObject = null;
            }
        }
        else
        {
            FocusedObject = null;
        }

        if (FocusedObject != OldFocusedObject)
        {
            if (OldFocusedObject != null)
            {
                OldFocusedObject.SendMessage("GazeExited");
            }

            if (FocusedObject != null)
            {
                FocusedObject.SendMessage("GazeEntered");
            }
        }
    }

    private void ScaleRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        SendMessageObject();
    }

    private void ScaleRecognizer_NavigationStartedEvent(InteractionSourceKind source, Vector3 scaleRecognizerPosition, Ray headRay)
    {
        IsScalling = true;
        ScaleRecognizerPosition = scaleRecognizerPosition;
    }

    private void ScaleRecognizer_NavigationUpdatedEvent(InteractionSourceKind source, Vector3 scaleRecognizerPosition, Ray headRay)
    {
        IsScalling = true;
        ScaleRecognizerPosition = scaleRecognizerPosition;
    }

    private void ScaleRecognizer_NavigationCompletedEvent(InteractionSourceKind source, Vector3 scaleRecognizerPosition, Ray headRay)
    {
        IsScalling = false;
    }

    private void ScaleRecognizer_NavigationCanceledEvent(InteractionSourceKind source, Vector3 scaleRecognizerPosition, Ray headRay)
    {
        IsScalling = false;
    }

    public void ChangeRecognizerToRotate()
    {
        if (ActiveRecognizer != null)
        {
            if (ActiveRecognizer == RotationRecognizer)
            {
                return;
            }

            ScaleRecognizer.CancelGestures();
            ScaleRecognizer.StopCapturingGestures();
        }

        RotationRecognizer.StartCapturingGestures();
        ActiveRecognizer = RotationRecognizer;
    }

    public void ChangeRecognizerToScale()
    {
        if (ActiveRecognizer != null)
        {
            if (ActiveRecognizer == ScaleRecognizer)
            {
                return;
            }

            RotationRecognizer.CancelGestures();
            RotationRecognizer.StopCapturingGestures();
        }

        ScaleRecognizer.StartCapturingGestures();
        ActiveRecognizer = ScaleRecognizer;
    }
    
    private void ChangeRecognizer(GestureRecognizer neededRecognizer)
    {
        if (ActiveRecognizer != null)
        {
            if (ActiveRecognizer == neededRecognizer)
            {
                return;
            }

            ActiveRecognizer.CancelGestures();
            ActiveRecognizer.StopCapturingGestures();
        }

        neededRecognizer.StartCapturingGestures();
        ActiveRecognizer = neededRecognizer;
    }

    private void RotationRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        SendMessageObject();
    }

    private void RotationRecognizer_NavigationStartedEvent(InteractionSourceKind source, Vector3 rotateRecognizerPosition, Ray headRay)
    {
        IsRotating = true;
        RotateRecognizerPosition = rotateRecognizerPosition;
    }

    private void RotationRecognizer_NavigationUpdatedEvent(InteractionSourceKind source, Vector3 rotateRecognizerPosition, Ray headRay)
    {
        IsRotating = true;
        RotateRecognizerPosition = rotateRecognizerPosition;
    }

    private void RotationRecognizer_NavigationCompletedEvent(InteractionSourceKind source, Vector3 rotateRecognizerPosition, Ray headRay)
    {
        IsRotating = false;
    }

    private void RotationRecognizer_NavigationCanceledEvent(InteractionSourceKind source, Vector3 rotateRecognizerPosition, Ray headRay)
    {
        IsRotating = false;
    }

    void OnDestroy()
    {
        RotationRecognizer.TappedEvent -= RotationRecognizer_TappedEvent;
        RotationRecognizer.NavigationCanceledEvent -= RotationRecognizer_NavigationCanceledEvent;
        RotationRecognizer.NavigationCompletedEvent -= RotationRecognizer_NavigationCompletedEvent;
        RotationRecognizer.NavigationUpdatedEvent -= RotationRecognizer_NavigationUpdatedEvent;
        RotationRecognizer.NavigationStartedEvent -= RotationRecognizer_NavigationStartedEvent;

        ScaleRecognizer.TappedEvent -= ScaleRecognizer_TappedEvent;
        ScaleRecognizer.NavigationCanceledEvent -= ScaleRecognizer_NavigationCanceledEvent;
        ScaleRecognizer.NavigationCompletedEvent -= ScaleRecognizer_NavigationCompletedEvent;
        ScaleRecognizer.NavigationUpdatedEvent -= ScaleRecognizer_NavigationUpdatedEvent;
        ScaleRecognizer.NavigationStartedEvent -= ScaleRecognizer_NavigationStartedEvent;
    }
}
