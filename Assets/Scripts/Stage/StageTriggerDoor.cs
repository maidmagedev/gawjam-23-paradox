using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageTriggerDoor : MonoBehaviour
{
    [Header("Auto-assigned on Game Start.")]
    [SerializeField] TransitionAnimator transitionAnimator;
    [SerializeField] CameraSystem cameraSystem;
    [SerializeField] AnimationEventAdoption aeAdopter;
    public StageBoundingBox myStage; // automatically assigned by StageBoundingBox on level start.
    public FacingDirection facingDirection;
    bool cooldown = false;
    [Header("IGNORE THESE FOR THIS BUILD")]
    [SerializeField] GameObject popupText; // can be null, only if applicable (designed for sidescroller, awayfromside direction)
    public float textPromptTimer = 0.0f;

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
        
        if (popupText != null) {
            if (textPromptTimer <= 0) {
                popupText.SetActive(false);
            } else {
                textPromptTimer -= Time.deltaTime;
            }
        }

        if (popupText != null && popupText.activeSelf) {
            if (Input.GetKeyDown(KeyCode.E)) {
                Debug.Log("Interact with door");
                SwapRooms();
            }
        }
    }

    void OnTriggerEnter() {
        //transitionAnimator.StartTransition();
        //Debug.Log("enter normal");
        SwapRooms();
    }

    // private void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col is PolygonCollider2D) {
    //         return;
    //     }
    //     Debug.Log("enter 2d");
    //     if (myStage != null) {
    //         StartCoroutine(WaitToTeleport());
    //         transitionAnimator.PlayScreenWipe();
    //     }
    // }

    IEnumerator WaitToTeleport() {
        yield return new WaitForSeconds(0.5f);
        
        // move the 2d guy
        if (facingDirection == FacingDirection.towardsSide) {
            aeAdopter.MoveRowB_ToBack();
            GameObject playerObj = FindObjectOfType<TopDownController>().gameObject;
            playerObj.transform.position += new Vector3(0, 0, 10);
            myStage.FindConnectedStageAndDontMove(this);
        } else if (facingDirection == FacingDirection.awayFromSide) {
            aeAdopter.MoveRowB_ToFront();
            // we dont need to move the player to the other grid for the Z axis movements, that actually happens automatically.
            // just shift the position backwards.
            GameObject playerObj = FindObjectOfType<TopDownController>().gameObject;
            playerObj.transform.position -= new Vector3(0, 0, 15);
            myStage.FindConnectedStageAndDontMove(this);
        } else {
            myStage.FindConnectedStage(this);
            cameraSystem.ShiftCamera(facingDirection);
        }
    }
    public void SwapRooms() {
        if (myStage != null) {
            if (cooldown) {
                return;
            }
            StartCoroutine(SelfCooldown());
            StartCoroutine(WaitToTeleport());
            transitionAnimator.PlayScreenWipe();
        }
    }

    IEnumerator SelfCooldown() {
        cooldown = true;
        yield return new WaitForSeconds(1.0f);
        cooldown = false;
    }

    public void EnablePrompt() {
        popupText.SetActive(true);
        textPromptTimer = 1.0f;
    }


}
