using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageBoundingBox : MonoBehaviour
{
    public List<StageTriggerDoor> doors = new List<StageTriggerDoor>();
    public String chamberName;
    public LayerMask doorLayer;
    [SerializeField] bool showRay;
    public GameObject my2DEnviro;
    private Ray myRay;
    [SerializeField] bool imTheStartingChamber;

    public void Start() {
        foreach(StageTriggerDoor door in doors) {
            door.myStage = this;
        }

        if (!imTheStartingChamber) {
            my2DEnviro.SetActive(false);
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
            StageBoundingBox nextBoundingBox = nextDoor.myStage;

            nextBoundingBox.my2DEnviro.SetActive(true); // enable the next 2D map.

            //Debug.Log(chamberName + " Confirmed hit, transfering player to " + nextBoundingBox.chamberName);
            Vector3 otherDoorPosition = nextDoor.transform.position;

            GameObject playerObjTD = FindObjectOfType<TopDownController>().gameObject;
            GameObject playerObjSide = FindObjectOfType<PlatformingMovementComponent>().gameObject;

            // ray.direction * 2 places the player forward slightly
            playerObjTD.transform.position = (ray.direction * 2f) + new Vector3(otherDoorPosition.x, playerObjTD.transform.position.y + 2, otherDoorPosition.z);
            playerObjSide.transform.position = (ray.direction * 2f) + new Vector3(otherDoorPosition.x, playerObjSide.transform.position.y, -20);

            my2DEnviro.SetActive(false); // disable my 2D map.
        } else {
            Debug.Log("none found");
        }
    }

    public void FindConnectedStageAndDontMove(StageTriggerDoor door) {
        Ray ray = new Ray(door.transform.position + new Vector3(0, 10, 0), door.transform.TransformDirection(Vector3.forward));
        Debug.DrawRay(ray.origin, ray.direction * 99, Color.blue);
        RaycastHit hit;
        //showRay = true;
        myRay = ray;

        if (Physics.Raycast(ray, out hit, 99f, doorLayer)) {
            Debug.Log(hit.transform.gameObject.name);
            //StageBoundingBox nextBoundingBox = hit.transform.gameObject.GetComponent<StageBoundingBox>();
            StageTriggerDoor nextDoor = hit.transform.gameObject.GetComponent<StageTriggerDoor>();
            StageBoundingBox nextBoundingBox = nextDoor.myStage;

            nextBoundingBox.my2DEnviro.SetActive(true); // enable the next 2D map.

            //Debug.Log(chamberName + " Confirmed hit, transfering player to " + nextBoundingBox.chamberName);
            Vector3 otherDoorPosition = nextDoor.transform.position;

            GameObject playerObjTD = FindObjectOfType<TopDownController>().gameObject;
            GameObject playerObjSide = FindObjectOfType<PlatformingMovementComponent>().gameObject;

            my2DEnviro.SetActive(false); // disable my 2D map.
        } else {
            Debug.Log("none found");
        }
    }

}
