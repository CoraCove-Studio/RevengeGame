using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Controls")]
    public KeyCode forwardKey = KeyCode.W;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode backKey = KeyCode.S;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode upKey = KeyCode.Space;
    [Header("Other Controls")]
    public KeyCode interactKey = KeyCode.E;
    public KeyCode attackKey = KeyCode.F;

    [Header("Movement Settings")]
    public float speed = 5.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.0f;
    private Vector3 velocity;

    private CharacterController controller;
    public bool grounded;
    Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        renderer = GetComponent<Renderer>();
        Debug.Log(renderer.bounds.size.y);
        controller.height = renderer.bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * speed);

        if (move != Vector3.zero) { gameObject.transform.forward = move; }

        grounded = Physics.Raycast(transform.position, Vector3.down, (controller.height)); // LayerMask.NameToLayer("Ground")
        // if (grounded && velocity.y < 0) { velocity.y = 0f; }
        if (grounded && Input.GetKeyDown(upKey)) { velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity); }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
