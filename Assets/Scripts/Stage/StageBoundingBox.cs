using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBoundingBox : MonoBehaviour
{
    public List<StageTriggerDoor> doors = new List<StageTriggerDoor>();
    public String chamberName;
    public LayerMask stageLayer;

    public void Start() {
        foreach(StageTriggerDoor door in doors) {
            door.myStage = this;
        }
    }

    void Update() {
        if (doors != null) {
            Ray ray = new Ray(doors[0].transform.position, doors[0].transform.TransformDirection(Vector3.forward));
            Debug.DrawRay(ray.origin, ray.direction * 99, Color.blue);
        }

    }

    public void FindConnectedStage() {
        Ray ray = new Ray(doors[0].transform.position, doors[0].transform.TransformDirection(Vector3.forward));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 30f, stageLayer)) {
            Debug.Log("Confirm.");
            Vector3 otherDoorPosition = hit.transform.gameObject.GetComponent<StageBoundingBox>().doors[0].transform.position;
            GameObject playerObj = FindObjectOfType<TopDownController>().gameObject;
            // ray.direction * 2 places ther player forward slightly
            playerObj.transform.position = (ray.direction * 2f) + new Vector3(otherDoorPosition.x, playerObj.transform.position.y + 2, otherDoorPosition.z);
        } else {
            Debug.Log("none");
        }
    }

}
