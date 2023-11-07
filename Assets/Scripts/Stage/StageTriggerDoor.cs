using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTriggerDoor : MonoBehaviour
{
    [SerializeField] TransitionAnimator transitionAnimator;

    // Start is called before the first frame update
    void Start()
    {
        if (transitionAnimator == null) {
            transitionAnimator = FindObjectOfType<TransitionAnimator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter() {
        transitionAnimator.StartTransition();
    }
}
