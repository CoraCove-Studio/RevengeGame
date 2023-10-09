using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
//class
{

    private CharacterAnimation player_Anim;
    private Rigidbody myBody;

    public float walk_Speed = 3f;
    public float z_Speed = 1.5f;
    
    //to have the player face the right side
    private float rotation_Y = -90f;
    private float rotation_Speed = 15f;

    private Dialogue dialogueScript;
    private DialogueEvent infoScript;
    private GameObject eventParticipant;
    public bool dialoguePrompt = false;

    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        player_Anim = GetComponentInChildren<CharacterAnimation>();
        dialogueScript = GameObject.Find("DialogueUI").GetComponent<Dialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        AnimatePlayerWalk();
        if (Input.GetKeyDown(KeyCode.E) && dialoguePrompt) // Starts dialogue from in-game prompt.
        {
            if (!infoScript.repeat) { eventParticipant.tag = "Untagged"; } // If the particular dialogue event doesn't repeat, remove the "CanTalkTo" tag.
            dialoguePrompt = false; // Isn't made false in Dialogue script later, necessary to avoid StartDialogue() being called twice on key down.
            dialogueScript.StartDialogue(); // infoScript.eventName
        }
    }

    private void FixedUpdate()
    {
        DetectMovement();
    }

    //movement
    void DetectMovement()
    {
        myBody.velocity = new Vector3(Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) * (-walk_Speed),
            myBody.velocity.y, 
            Input.GetAxisRaw(Axis.VERTICAL_AXIS) * (-z_Speed));
    }

    //rotation
    void RotatePlayer()
    {
        if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) > 0)
        {
            transform.rotation = Quaternion.Euler(0f, -Mathf.Abs(rotation_Y), 0f);
        }

        else if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) < 0)
        {
            transform.rotation = Quaternion.Euler(0f, Mathf.Abs(rotation_Y), 0f);
        }
    }

    //animate player walk
    void AnimatePlayerWalk()
    {
        if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) != 0 || Input.GetAxisRaw(Axis.VERTICAL_AXIS) != 0)
        {
            player_Anim.Walk(true);
        }
        else
        {
            player_Anim.Walk(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CanTalkTo"))
        {
            eventParticipant = other.gameObject; // The object with the "CanTalkTo" tag, referenced in Update().
            infoScript = other.gameObject.GetComponent<DialogueEvent>();
            dialoguePrompt = true;
            dialogueScript.dialoguePrompt.SetActive(true); // Prompts the player to start dialogue.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        dialoguePrompt = false;
        if (other.CompareTag("CanTalkTo")) { dialogueScript.dialoguePrompt.SetActive(false); }
    }
}

