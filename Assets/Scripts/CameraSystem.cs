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

    public void ShiftCamera(bool doorFacingRight) {
        int dir = 1;
        if (doorFacingRight == false) {
            dir = -1;
        }
        cameraHolder.position += new Vector3(dir * 40f, 0, 0);
    }
}
