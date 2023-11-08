using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class two_way_platform : MonoBehaviour
{
    private bool PlayerTouching = false;

    private BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerTouching && Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(TurnOffCollider());
        }
    }

    private IEnumerator TurnOffCollider()
    {
        collider.enabled = false;
        yield return new WaitForSeconds(1);
        collider.enabled = true;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerTouching = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerTouching = false;
        }
    }
}
