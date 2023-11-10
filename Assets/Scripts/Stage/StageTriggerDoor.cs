using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTriggerDoor : MonoBehaviour
{
    [Header("Auto-assigned on Game Start.")]
    [SerializeField] TransitionAnimator transitionAnimator;
    [SerializeField] CameraSystem cameraSystem;
    public StageBoundingBox myStage; // automatically assigned by StageBoundingBox on level start.
    public FacingDirection facingDirection;

    public enum FacingDirection {
        right,
        left,
        awayFromSide,
        towardsSide,
    }

    // Start is called before the first frame update
    void Start()
    {
        if (transitionAnimator == null) {
            transitionAnimator = FindObjectOfType<TransitionAnimator>();
        }
        if (cameraSystem == null) {
            cameraSystem = FindObjectOfType<CameraSystem>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter() {
        //transitionAnimator.StartTransition();
        if (myStage != null) {
            StartCoroutine(WaitToTeleport());
            transitionAnimator.PlayScreenWipe();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (myStage != null) {
            StartCoroutine(WaitToTeleport());
            transitionAnimator.PlayScreenWipe();
        }
    }

    IEnumerator WaitToTeleport() {
        yield return new WaitForSeconds(0.5f);
        myStage.FindConnectedStage(this);
        // move the 2d guy
        cameraSystem.ShiftCamera(facingDirection);
    }
    
}
