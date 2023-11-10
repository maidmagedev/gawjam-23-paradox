using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalTrigger : MonoBehaviour
{
    [SerializeField] JournalHandler journalHandler;
    [SerializeField] int journalEntrytoUnlock;

    void Start() {
        journalHandler = FindObjectOfType<JournalHandler>();
    }
    void OnTriggerEnter(Collider col) {
        if (!col.gameObject.CompareTag("Player")) {
            return;
        }     
        if (!journalHandler.ownedJournalIDs.Contains(journalEntrytoUnlock)) {
            journalHandler.AddPage(journalEntrytoUnlock);
        }
    }
    void OnTriggerEnter2D(Collider2D col) {
        if (!col.gameObject.CompareTag("Player")) {
            return;
        }     

        if (!journalHandler.ownedJournalIDs.Contains(journalEntrytoUnlock)) {
            journalHandler.AddPage(journalEntrytoUnlock);
        }
    }

}
