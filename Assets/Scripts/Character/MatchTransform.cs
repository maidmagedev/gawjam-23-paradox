using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchTransform : MonoBehaviour
{

    [SerializeField] private Transform followTarget;
    [SerializeField] bool matchX = true;
    [SerializeField] bool matchY = true;
    [SerializeField] bool matchZ = true;


    // Update is called once per frame
    void Update()
    {   
        Vector3 endPosition = new(transform.position.x, transform.position.y, transform.position.z);
        if (matchX) {
            endPosition.x = followTarget.position.x;
        }
        if (matchY) {
            endPosition.y = followTarget.position.y;
        }
        if (matchZ) {
            endPosition.z = followTarget.position.z;
        }

        this.transform.position = endPosition;
    }

    
}
