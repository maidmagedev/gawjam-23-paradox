using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTriggerDoor : MonoBehaviour
{
    [Header("Auto-assigned on Game Start.")]
    [SerializeField] TransitionAnimator transitionAnimator;
    [SerializeField] CameraSystem cameraSystem;
    [SerializeField] AnimationEventAdoption aeAdopter;
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
        if (aeAdopter == null) {
            aeAdopter = FindObjectOfType<AnimationEventAdoption>();
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
        
        // move the 2d guy
        if (facingDirection == FacingDirection.towardsSide) {
            //aeAdopter.MoveRowB_ToFront();
        } else if (facingDirection == FacingDirection.awayFromSide) {
            aeAdopter.MoveRowB_ToFront();
            // we dont need to move the player to the other grid for the Z axis movements, that actually happens automatically.
            // just shift the position backwards.
            GameObject playerObj = FindObjectOfType<TopDownController>().gameObject;
            playerObj.transform.position -= new Vector3(0, 0, 20);
        } else {
            myStage.FindConnectedStage(this);
            cameraSystem.ShiftCamera(facingDirection);
        }
    }
    
}
