using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{   
    // preferred to hook these up prior, but its fine otherwise.
    [Header("External References - setup at Start()")]
    [SerializeField] Animator anim;
    [SerializeField] PerspectiveManager perspecMan;

    [Header("Internal variables")]
    private Rigidbody2D sideRB;
    private Rigidbody topRB;


    // Start is called before the first frame update
    void Start()
    {
        if (perspecMan == null) {
            perspecMan = FindObjectOfType<PerspectiveManager>();
        }
        if (anim == null) {
            anim = GetComponentInChildren<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        VerifyReferences();

        if (perspecMan.currentPerspective == PerspectiveManager.PerspectiveMode.sidescroller) {
            // if (Mathf.Abs(sideRB.velocity.y) > 0) {
            //     anim.CrossFade("jump", 0);
            // }
            if (Mathf.Abs(sideRB.velocity.x) > 0) {
                //anim.CrossFade("walk", 0);
            }
        } else {
            // some people do flatvelocity, but technically the 3d model cant jump. so it shouldnt matter.
            if (topRB.velocity != Vector3.zero) {
                //anim.CrossFade("walk", 0);
            } else {
                //anim.CrossFade("idle", 0);
            }
        }
    }

    void VerifyReferences() {
        if (sideRB == null) {
            sideRB = perspecMan.sideScrollerController.rb;
        }
        if (topRB == null) {
            topRB = perspecMan.topDownController.rb;
        }
    }
}
