using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using System;

public class Dialogue : MonoBehaviour
{
    public GameObject dialogueBox;
    public TMP_Text dialogueName;
    public TMP_Text dialogueContent;

    private string[] dialogueList;
    private int arrayPos = 0;

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox = GameObject.Find("DialogueBox");
        dialogueName = dialogueBox.transform.GetChild(0).GetComponent<TMP_Text>(); // The name of the character speaking.
        dialogueContent = dialogueBox.transform.GetChild(1).GetComponent<TMP_Text>(); // The dialogue being said.

        GetDialogueList("Other/Level01-Dialogue");
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueBox.activeSelf && Input.GetKeyDown(KeyCode.E)) { StartDialogue(); } // Progresses dialogue if dialogue box already open.
    }

    void GetDialogueList(string doc_path) // Splits dialogue text file into separate dialogue.
    {
        TextAsset doc = Resources.Load<TextAsset>(doc_path);
        var text = doc.text;
        var contents = Regex.Split(text, @"\n"); // Splits the string into an array of strings, splitting on newlines.
        dialogueList = contents;
    }

    public void StartDialogue() // Iniates or progresses dialogue. Will automatically close dialogue box if reached end of dialogue.
    {
        if (!dialogueBox.activeSelf) { dialogueBox.SetActive(true); } // Shows the dialogue box if not already displayed.
        try
        {
            var text = dialogueList[arrayPos]; // Grabs the currently "selected" dialogue from the array.
            var matches = Regex.Matches(text, @"\[(.+)] (.+)");
            if (matches.Count > 0 && matches[0].Groups.Count > 1) // If the search didn't turn up empty...
            {
                string name = matches[0].Groups[1].Value;
                string dialogue = matches[0].Groups[2].Value;
                dialogueName.text = name;
                dialogueContent.text = dialogue;
                arrayPos++; // Progresses position of dialogue.
            }
        }
        
        catch (IndexOutOfRangeException) // If the array is out of range (it ran out of dialogue)...
        {
            dialogueBox.SetActive(false);
        }
    }
}
