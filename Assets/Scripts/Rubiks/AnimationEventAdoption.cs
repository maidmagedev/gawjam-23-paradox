using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventAdoption : MonoBehaviour
{
    public List<SegBlockRotator> rows = new List<SegBlockRotator>();
    [SerializeField] Animator anim;

    public void AdoptAllFromCurrentRows() {
        Debug.Log("Adopt.");
        foreach(SegBlockRotator currentRow in rows) {
            currentRow.AdoptSegBlocks();
        }
    }

    void Update() {
        // if (Input.GetKeyDown(KeyCode.Alpha4)) {
        //     Debug.Log("activate anim");
        //     anim.CrossFade("MoveRowB_ToFront", 0);
        // }
    }

    public void MoveRowB_ToFront() {
        anim.CrossFade("MoveRowB_ToFront", 0);
    }

    public void ReturnToOriginalParent() {
        foreach(SegBlockRotator currentRow in rows) {
            currentRow.ReturnToOriginalParent();
        }
    }   
}
