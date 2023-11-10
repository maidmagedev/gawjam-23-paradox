using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] Transform cameraHolder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShiftCamera(StageTriggerDoor.FacingDirection faceDir) {
        switch (faceDir) {
            case StageTriggerDoor.FacingDirection.left:
                    cameraHolder.position += new Vector3(-1 * 40f, 0, 0);
                break;
            case StageTriggerDoor.FacingDirection.right:
                    cameraHolder.position += new Vector3(1 * 40f, 0, 0);
                break;
            case StageTriggerDoor.FacingDirection.awayFromSide:
                Debug.Log("not implemented.");
                break;
            case StageTriggerDoor.FacingDirection.towardsSide:
                Debug.Log("not implemented.");
                break;
        }
    }
}
