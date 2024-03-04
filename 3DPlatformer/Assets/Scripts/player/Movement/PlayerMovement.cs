using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float airMultiplier;

    public int maxJumps = 2;
    private int jumpsRemaining;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public LayerMask platform;

    bool grounded;
    bool onPlatform;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        jumpsRemaining = maxJumps;
    }

    // Update is called once per frame
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down * 0.5f, +0.2f, whatIsGround);
        onPlatform = Physics.Raycast(transform.position, Vector3.down * 0.5f, +0.2f, platform);

        MyInput();
        SpeedControl();

        if (grounded || onPlatform)
        {
            rb.drag = groundDrag;
            jumpsRemaining = maxJumps; // Reset jumps when grounded
        }
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when jumping
        if (Input.GetKeyDown(jumpKey) && jumpsRemaining > 0)
        {
            if (!grounded || !onPlatform)
            {
                jumpsRemaining--; // Decrease jumps when in the air
            }

            Jump();
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded || onPlatform)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else if (!grounded || !onPlatform)
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier * 10f, ForceMode.Force); 
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); 

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}
