using System.Collections;
using System.Collections.Generic;
using UnityEngine.VR.WSA.Input;
using UnityEngine;
using System;

public class _HandGuidance : MonoBehaviour {

    private Quaternion defaultRotation;

    public GameObject HandGuidance;
    private GameObject handGuidance;

    public GameObject ParentPrefab;

    private float visibleFactor = 0.3f;
    private uint? handId = null;

	// Use this for initialization
	void Start () {
		
        if (HandGuidance == null || ParentPrefab == null)
        {
            Debug.Log("The prefab(-s) wasn't assigned in " + gameObject.name + ".");
        }

        defaultRotation = HandGuidance.transform.rotation;

        handGuidance = Instantiate(HandGuidance);
        handGuidance.transform.parent = ParentPrefab.transform;
        handGuidance.SetActive(false);

        InteractionManager.SourceUpdated += InteractionManager_SourceUpdated;
        InteractionManager.SourceReleased += InteractionManager_SourceReleased;
        InteractionManager.SourceLost += InteractionManager_SourceLost;
	}

    private void InteractionManager_SourceLost(InteractionSourceState hand)
    {
        if (handId.Value != hand.source.id) { return; }

        HideHandGuidance(hand);
    }

    private void InteractionManager_SourceReleased(InteractionSourceState hand)
    {
        HideHandGuidance(hand);
    }

    private void HideHandGuidance(InteractionSourceState hand)
    {
        handGuidance.SetActive(false);
        handId = null;
    }

    private void InteractionManager_SourceUpdated(InteractionSourceState hand)
    {
        if (!handId.HasValue)
        {
            handId = hand.source.id;
        }
        else if (handId.Value != hand.source.id) { return; }

        if (hand.properties.sourceLossRisk > visibleFactor)
        {
            float distanceFromCenter = (float)(hand.properties.sourceLossRisk * 0.3);
            handGuidance.transform.position = ParentPrefab.transform.position - hand.properties.sourceLossMitigationDirection * distanceFromCenter;
            handGuidance.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, hand.properties.sourceLossMitigationDirection) * defaultRotation;

            handGuidance.SetActive(true);
        }
        else
        {
            handGuidance.SetActive(false);
        }
    }
}
