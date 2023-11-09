using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{   
    // preferred to hook these up prior, but its fine otherwise.
    [Header("External References - some setup at Start()")]
    [SerializeField] Animator anim;
    [SerializeField] PerspectiveManager perspecMan;
    [SerializeField] GameObject mesh; // not setup at start

    [Header("Internal variables")]
    private Rigidbody2D sideRB;
    private Rigidbody topRB;
    private bool movingLeft;


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

            MeshFlip();

            // the jump animation wasnt that good, and its not easy to setup here.
            // we can check perspecMan.sideScrollerController.GetIsJumping();, but it'll get overwritten pretty easily with the current implementation :p
            // we'd need an animation queue setup.

            if (!perspecMan.sideScrollerController.isGrounded) {
                anim.CrossFade("air", 0);
            } else {
                if (Mathf.Abs(sideRB.velocity.x) > 0.1f) {
                    anim.CrossFade("walk", 0);
                } else {
                    anim.CrossFade("idle", 0);
                }
            }

            

            // // animation load
            // if (Mathf.Abs(sideRB.velocity.x) > 0) {
            //     anim.CrossFade("walk", 0);
            // }
        } else {
            Vector3 playerMoveDir = perspecMan.topDownController.moveDir;
            if (playerMoveDir != Vector3.zero) {
                mesh.transform.localRotation = Quaternion.LookRotation(playerMoveDir);
            }
            // some people do flatvelocity, but technically the 3d model cant jump. so it shouldnt matter.
            if (topRB.velocity != Vector3.zero) {
                anim.CrossFade("walk", 0);
            } else {
                anim.CrossFade("idle", 0);
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

    public void SetupSideScrollerAnims() {
        //mesh.transform.rotation = Quaternion.Euler(0, 135, 0);
    }

    public void SetupTopDownAnims() {
        //mesh.transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    // to be replaced?
    private void MeshFlip() {
        // rotation / flipping
        if (sideRB.velocity.x > 0.1f) {
            if (movingLeft) {
                movingLeft = false;
            }
            mesh.transform.localRotation = Quaternion.Euler(0, 135, 0);
        } else if (sideRB.velocity.x < -0.1f) {
            if (!movingLeft) {
                movingLeft = true;
            }
            mesh.transform.localRotation = Quaternion.Euler(0, -135, 0);
        }
    }
}
