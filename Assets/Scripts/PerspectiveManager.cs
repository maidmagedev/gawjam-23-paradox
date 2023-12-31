using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PerspectiveManager : MonoBehaviour
{
    [Header("External References")]
    [SerializeField] public PlayerAnimations playerAnims;
    [SerializeField] public PlatformingMovementComponent sideScrollerController;

    [SerializeField] public TopDownController topDownController;

    [SerializeField] private GameObject sideScrollerCamera;

    [SerializeField] private GameObject topDownCamera;

    [SerializeField] private MatchTransform matchTopToSide;
    [SerializeField] private MatchTransform matchSideToTop;


    [Header("Environment Data")]
    //[SerializeField] Tilemap tilemap;
    [SerializeField] Transform origin;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] LayerMask doorLayer;


    [Header("Tracking")]
    public PerspectiveMode currentPerspective;

    public enum PerspectiveMode {
        sidescroller,
        topdown
    }

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

        if (currentPerspective == PerspectiveMode.sidescroller) {
            SidesScrollerDoorDetector();
        }
    }


    private void swapPerspectives()
    {
        bool shouldWarp = IsPlayerFloatingWhenPerspectiveChanges();

        sideScrollerController.ToggleMovement();
        topDownController.ToggleMovement();
        
        // TODO: use cinemachine instead
        sideScrollerCamera.GetComponent<CinemachineVirtualCamera>().Priority *= -1;
        topDownCamera.GetComponent<CinemachineVirtualCamera>().Priority *= -1;
        //sideScrollerCamera.SetActive(!sideScrollerCamera.activeSelf);
        //topDownCamera.SetActive(!topDownCamera.activeSelf);
        
        
        matchTopToSide.enabled = !matchTopToSide.enabled;
        matchSideToTop.enabled = !matchSideToTop.enabled;

        if (currentPerspective == PerspectiveMode.sidescroller) {
            // Going to TopDown
            currentPerspective = PerspectiveMode.topdown;
            if (shouldWarp) {
                Debug.Log("Player is floating, do warp.");
                PositionWarpOnZ();
            }
            playerAnims.SetupTopDownAnims();
        } else {
            // Going to Sidescroller
            PositionWarpOnY();
            currentPerspective = PerspectiveMode.sidescroller;
            playerAnims.SetupSideScrollerAnims();
        }
    }

    // This function warps the player to the correct platform..
    // Sometimes, the player might be "floating" in top down, or otherwise end up on the wrong surface.
    // In order to fix this, we teleport the player to the nearest platform, using raycasts.
    private void PositionWarpOnZ() {
        float rayCastDistance = 50f;
        Ray ray = new Ray(origin.position, transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayCastDistance, targetLayer)) {
            Debug.Log("Hit object:" + hit.collider.gameObject.name);
            Debug.Log(topDownController.transform.position + " : " + hit.point);

            // Teleport the player to where the raycast ended, adjusted upwards by 1.5f units and deeper (along Z) by 2 units.
            topDownController.transform.position = hit.point + new Vector3(0f, 1.5f, 2f);
        }
        
        //Debug.DrawRay(ray.origin, ray.direction * rayCastDistance, Color.cyan);
    }

    // Intended for TopDown->SideScroller
    private void PositionWarpOnY() {
        float rayCastDistance = Mathf.Infinity; // might regret this number later haha
        Ray ray = new Ray(topDownController.transform.position, Vector3.down);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, rayCastDistance, targetLayer)) {
            // adjust the vertical height, maintain everything
            sideScrollerController.transform.position = new Vector3(sideScrollerController.transform.position.x, hit.point.y + 1.0f, sideScrollerController.transform.position.z);
        }
    }

    // Used currently for SideScroller->TopDown
    private bool IsPlayerFloatingWhenPerspectiveChanges() {
        if (currentPerspective == PerspectiveMode.sidescroller) { 
                
            Ray ray = new Ray(topDownController.transform.position, Vector3.down);

            Debug.DrawRay(ray.origin, Vector3.down * 1.25f, Color.cyan);
            return !Physics.Raycast(ray, out _, 1.5f, targetLayer);
        } else {
            //Debug.Log("[IGNORE]: Dummy value, use not applicable for TopDown->SideScroller.");
            return false; 
        }
    }

    void SidesScrollerDoorDetector() {
        Ray ray = new Ray(sideScrollerController.transform.position, Vector3.forward);
        Debug.DrawRay(ray.origin, ray.direction * 45f, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 45f, doorLayer)) {
            //Debug.Log("hit door");
            StageTriggerDoor foundDoor = hit.transform.gameObject.GetComponent<StageTriggerDoor>();
            if (foundDoor == null) {
                Debug.Log("Hit invalid object, for some reason: [" + hit.transform.gameObject.name + "]");
                return;
            }

            if (foundDoor.facingDirection == StageTriggerDoor.FacingDirection.awayFromSide) {
                // button prompt to enter
                foundDoor.EnablePrompt();
            } else if (foundDoor.facingDirection != StageTriggerDoor.FacingDirection.towardsSide) {
                // so,facing direction = left and right
                foundDoor.SwapRooms();
            } 
        }
    }
    
}

