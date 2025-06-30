using System.Data.Common;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player movement
    public float jumpSpeed = 7f; // Speed of the player jump

    private Rigidbody rb; // Reference to the Rigidbody component
    private Vector2 moveInput; // Input for movement
    [SerializeField] private bool isGrounded = false; // Check if the player is on the ground

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputSystem_Actions playerActions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Ground Check Settings")]
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private void Awake()
    {
        playerActions = new InputSystem_Actions();
        moveAction = playerActions.Player.Move;
        jumpAction = playerActions.Player.Jump;

    }

    private void OnEnable()
    {
        playerActions.Enable();
    }

    void Start()
    {

        
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.constraints |= RigidbodyConstraints.FreezePositionZ; // Freeze Y position to prevent falling through the ground



        moveInput = moveAction.ReadValue<Vector2>();

    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();

        moveInput = moveAction.ReadValue<Vector2>();
       
        Vector3 velocity = rb.linearVelocity;
        velocity.x = moveInput.x * moveSpeed;
        rb.linearVelocity = velocity;

        if (jumpAction.triggered && isGrounded)
        {
            Jump();
        }

        if(moveInput.x < 0)
        {
            // Flip the player sprite or model if moving left
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(moveInput.x > 0)
        {
            // Flip the player sprite or model if moving right
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (playerActions.Player.Ability.triggered)
        {
            TimeStopManager.Instance.ToggleTimeStop();
        }
    }



    void Jump()
    {

        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        //isGrounded = false; // Set grounded to false after jumping

    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayer);
    }
}
