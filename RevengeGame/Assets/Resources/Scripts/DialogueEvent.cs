using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : MonoBehaviour
{
    public string eventName;
    public bool repeat = false;

    private new SphereCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        // Creates a trigger collider for an area around the object in which the player can initiate dialogue.
        collider = gameObject.AddComponent<SphereCollider>();
        collider.center = new Vector3 (0, 1, 0);
        collider.radius = 1.5f;
        collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
