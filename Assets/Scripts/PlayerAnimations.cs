using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] PerspectiveManager perspecMan;
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
        if (perspecMan.currentPerspective == PerspectiveManager.PerspectiveMode.sidescroller) {

        } else {
            
        }
    }
}
