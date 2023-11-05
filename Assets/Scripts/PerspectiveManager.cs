using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class PerspectiveManager : MonoBehaviour
{
    [SerializeField] private PlatformingMovementComponent sideScrollerController;

    [SerializeField] private TopDownController topDownController;

    [SerializeField] private GameObject sideScrollerCamera;

    [SerializeField] private GameObject topDownCamera;

    [SerializeField] private MatchTransform matchTopToSide;
    [SerializeField] private MatchTransform matchSideToTop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            print("changing perspectives!");
            swapPerspectives();
        }
    }

    private void swapPerspectives()
    {
        sideScrollerController.ToggleMovement();
        topDownController.ToggleMovement();
        sideScrollerCamera.SetActive(!sideScrollerCamera.activeSelf);
        topDownCamera.SetActive(!topDownCamera.activeSelf);
        matchTopToSide.enabled = !matchTopToSide.enabled;
        matchSideToTop.enabled = !matchSideToTop.enabled;
    }
}

