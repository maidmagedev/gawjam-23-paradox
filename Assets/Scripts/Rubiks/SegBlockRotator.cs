using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script handles a section of the cube to be detected and rotated.
// We enable a trigger collider, highlighting the section of blocks to be rotated.
// We collect all the collisions into a list over a single frame, then we return the result.
// All of these collisions are then made children of the current rotator.
// Afterwards, we rotate the current rotator.
public class SegBlockRotator : MonoBehaviour
{
    [SerializeField] private BoxCollider triggerSegBlockDetector;
    public List<GameObject> collidedSegBlocks = new List<GameObject>(); // seg blocks currently in area.
    [SerializeField] LayerMask segBlockLayer;

    [Header("!!! SET THIS BASED ON THE CURRENT ROTATOR'S POSITION!!! ")]
    [SerializeField] Vector3 rotateVector;
    bool isRotating = false;

    // Start is called before the first frame update
    void Start()
    {
        InitialSetup();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.O)) {
        // //     print("Debug: ACtivating Check...");
        //     StartCoroutine(CheckForCollisions());
        // }
    }

    void InitialSetup() {
        if (triggerSegBlockDetector == null) {
            triggerSegBlockDetector = GetComponent<BoxCollider>();
        }
        triggerSegBlockDetector.enabled = false;
    }

    public void RotateSection(Vector3 rotationIncrement) {
        if (isRotating) {
            Debug.Log("[ACTION DENIED]: already rotating!");
            return;
        }
        StartCoroutine(CollisionManagerAndRotator(rotationIncrement));
    }
    
    // called by UI buttons.
    public void RotateSection90Pos() {
        RotateSection(rotateVector);
    }
    // called by UI buttons.
    public void RotateSection90Neg() {
        RotateSection(rotateVector * -1);
    }

    IEnumerator CollisionManagerAndRotator(Vector3 rotationIncrement) {
        isRotating = true; // this lockout variable needs to be adjusted to be GLOBAL. Currently it only locks out a single rotator, but we need to disable ALL rotators.
        // disabling the trigger box collider allows us to refresh the collisions.
        triggerSegBlockDetector.enabled = false;
        // clear the list of previously assigned children.
        collidedSegBlocks.Clear();
        yield return new WaitForEndOfFrame();
        triggerSegBlockDetector.enabled = true;
        // wait a fraction of a second, this allows the OnTriggerEnter method to add all collided objects into a list.
        yield return new WaitForSeconds(0.1f);

        // set parenthood, allows us to rotate children by just rotating ourself.
        foreach(GameObject obj in collidedSegBlocks) {
            obj.transform.SetParent(this.transform);
        }

        // ROTATE THE SECTION
        float elapsedTime = 0.0f;
        float duration = 0.5f;
        Quaternion startingRotation = transform.rotation;
        Quaternion finalRotation = startingRotation * Quaternion.Euler(rotationIncrement.x, rotationIncrement.y, rotationIncrement.z);
        while (elapsedTime < duration) {
            transform.rotation = Quaternion.Lerp(startingRotation, finalRotation, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // set to final rotation, since the while loop lerp can undershoot & overshoot
        transform.rotation = finalRotation;


        yield return null;
        isRotating = false;
    }

    void OnTriggerEnter(Collider col) {
        //Debug.Log(col.gameObject.name + " : " + col.gameObject.layer);

        if (col.gameObject.layer == 6) {
            collidedSegBlocks.Add(col.gameObject);
        }
    }
}
