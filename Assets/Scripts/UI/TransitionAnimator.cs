using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Handles transitions such as fades to black, screen swipes, etc.
public class TransitionAnimator : MonoBehaviour
{
    //public TransitionType transitionType = TransitionType.DipToBlack;
    [SerializeField] private Animator animator;
    private bool transitionLockout;
    public bool waitForStageSetup;

    public enum TransitionType {
        DiptoBlack,
        DipfromBlack,
        SwipeLeftBlack,
        SwipeRightBlack,
        DiptoWhite,
        DipfromWhite,
        SwipeLeftWhite,
        SwipeRightWhite,
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.O)) {
            Debug.Log("Starting Transition");
            StartTransition();
        }
    }

    // Plays the default DiptoBlack Transition.
    public void StartTransition() {
        animator.CrossFade("DiptoBlack", 0);
    }

    // Plays the default DipfromBlack transition, intended to be used after StartTransition().
    public void EndTransition() {
        animator.CrossFade("DipfromBlack", 0);
    }

    // Plays a manually selected transition.
    public void PlayTransition(TransitionType transitionType) {
        animator.CrossFade(transitionType.ToString(), 0);
    }

    public void PlayScreenWipe() {
        if (!transitionLockout) {
            StartCoroutine(ScreenWipeHandler());
        } else {
            Debug.Log("transition is locked.");
        }
    }

    private IEnumerator ScreenWipeHandler() {
        transitionLockout = true;
        animator.CrossFade(TransitionType.SwipeLeftBlack.ToString(), 0);
        yield return new WaitForSeconds(1.0f);
        // while (waitForStageSetup) {
        //     // Camera is black for this period.
        //     yield return new WaitForSeconds(0.1f);
        // }
        animator.CrossFade(TransitionType.SwipeRightBlack.ToString(), 0);
        yield return new WaitForSeconds(1.0f);
        transitionLockout = false;
    }

    // IEnumerator PerformAnimation(float duration) {
    //     animator.CrossFade("DiptoBlack", 0);
    //     yield return new WaitForSeconds(duration);
    // }

    
}
