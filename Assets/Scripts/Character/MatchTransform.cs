using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchTransform : MonoBehaviour
{

    [SerializeField] private Transform followTarget;
    

    // Update is called once per frame
    void Update()
    {
        this.transform.position = followTarget.position;
    }

    
}
