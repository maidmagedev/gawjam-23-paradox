using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;

public class JournalHandler : MonoBehaviour
{
    [SerializeField] List<int> ownedJournalIDs = new List<int>();
    [SerializeField] TextMeshProUGUI textBody;
    [SerializeField] TextMeshProUGUI textPageCount;
    public int displayedID;
    // Start is called before the first frame update
    void Start()
    {
        //textBody.text = JournalEntries.journalEntries.GetValueOrDefault(0);   
        ownedJournalIDs.Add(0);
        ownedJournalIDs.Add(1);
        ownedJournalIDs.Add(2);

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GetPage(int journalID) {
        string text = JournalEntries.journalEntries.GetValueOrDefault(journalID);   
    }

    public void AddPage(int journalID) {
        ownedJournalIDs.Add(journalID);
    }

    public void UpdateUI() {
        textBody.text = JournalEntries.journalEntries.GetValueOrDefault(displayedID);
        textPageCount.text = (displayedID + 1) + "/" + ownedJournalIDs.Count;
    }

    public void PageLeft() {
        if (displayedID > 0) {
            displayedID--;
        }
        UpdateUI();
    }

    public void PageRight() {
        if (displayedID < ownedJournalIDs.Count - 1) {
            displayedID++;
        }
        UpdateUI();
    }
}
