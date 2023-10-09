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

    private string currentDialogue;
    private bool midDialogue = false;

    private Coroutine co; // Fun (terrible) fact: Unity is EVIL INCARNATE and will only stop coroutines if they're stopped in the same exact fashion they're started. See: https://discussions.unity.com/t/how-to-stop-a-co-routine-in-c-instantly/49118/4

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
        if (dialogueBox.activeSelf && Input.GetKeyDown(KeyCode.E)) // Progresses dialogue if dialogue box already open.
        {
            StopCoroutine(co);
            if (midDialogue) // Skips to end of dialogue if text is still generating.
            {
                midDialogue = false;
                dialogueContent.text = currentDialogue;
            }
            else { StartDialogue(); } // Moves to next dialogue if current dialogue finished generating.
        }
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
        Time.timeScale = 0f;
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
                dialogueContent.text = "";
                currentDialogue = dialogue;
                co = StartCoroutine(TextPace(dialogue));
                arrayPos++; // Progresses position of dialogue.
            }
        }
        
        catch (IndexOutOfRangeException) // If the array is out of range (it ran out of dialogue)...
        {
            dialogueBox.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    IEnumerator TextPace(string dialogue)
    {
        midDialogue = true;
        char[] ch = new char[dialogue.Length];
        for (int i = 0; i < dialogue.Length; i++) { ch[i] = dialogue[i]; }
        foreach (char c in ch)
        {
            dialogueContent.text += c;
            yield return new WaitForSecondsRealtime(0.025f);
        }
        midDialogue = false;
    }
}
