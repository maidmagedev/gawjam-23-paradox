using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Handles transitions such as fades to black, screen swipes, etc.
public class TransitionAnimator : MonoBehaviour
{
    //public TransitionType transitionType = TransitionType.DipToBlack;
    [SerializeField] private Animator animator;

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


    // IEnumerator PerformAnimation(float duration) {
    //     animator.CrossFade("DiptoBlack", 0);
    //     yield return new WaitForSeconds(duration);
    // }

    
}
