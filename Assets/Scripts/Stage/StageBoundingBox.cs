using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBoundingBox : MonoBehaviour
{
    public List<StageTriggerDoor> doors = new List<StageTriggerDoor>();
    public String chamberName;
    public LayerMask doorLayer;
    [SerializeField] bool showRay;
    private Ray myRay;

    public void Start() {
        foreach(StageTriggerDoor door in doors) {
            door.myStage = this;
        }
    }

    void Update() {
        // if (doors != null) {
        //     Ray ray = new Ray(doors[0].transform.position, doors[0].transform.TransformDirection(Vector3.forward));
        //     if (showRay) {
        //         Debug.DrawRay(ray.origin, ray.direction * 99, Color.blue);
        //     }
        // }

        if (showRay) {
            Debug.DrawRay(myRay.origin, myRay.direction * 99, Color.blue);
        }

    }

    public void FindConnectedStage(StageTriggerDoor door) {
        Ray ray = new Ray(door.transform.position + new Vector3(0, 10, 0), door.transform.TransformDirection(Vector3.forward));
        Debug.DrawRay(ray.origin, ray.direction * 99, Color.blue);
        RaycastHit hit;
        //showRay = true;
        myRay = ray;

        if (Physics.Raycast(ray, out hit, 99f, doorLayer)) {
            Debug.Log(hit.transform.gameObject.name);
            //StageBoundingBox nextBoundingBox = hit.transform.gameObject.GetComponent<StageBoundingBox>();
            StageTriggerDoor nextDoor = hit.transform.gameObject.GetComponent<StageTriggerDoor>();
            //StageBoundingBox nextBoundingBox = nextDoor.myStage;

            //Debug.Log(chamberName + " Confirmed hit, transfering player to " + nextBoundingBox.chamberName);
            Vector3 otherDoorPosition = nextDoor.transform.position;

            GameObject playerObj = FindObjectOfType<TopDownController>().gameObject;

            // ray.direction * 2 places the player forward slightly
            playerObj.transform.position = (ray.direction * 2f) + new Vector3(otherDoorPosition.x, playerObj.transform.position.y + 2, otherDoorPosition.z);
        } else {
            Debug.Log("none");
        }
    }

}
