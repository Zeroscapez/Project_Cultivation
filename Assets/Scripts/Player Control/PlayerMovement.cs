using System.Collections.Generic;
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


    [Header("Rewind Mechanic")]
    public float rewindTime = 5f; // seconds ago to rewind
    public InputAction rewindAction; 
    private List<PositionRecord> positionHistory = new List<PositionRecord>();
    public float rewindCooldown = 20f;
    private float lastRewindTime = Mathf.NegativeInfinity;

    [Header("Rewind Ghost")]
    public GameObject rewindPrefab;
    public Transform rewindGhost; // Assign your ghost GameObject in Inspector
    



    private struct PositionRecord
    {
        public Vector3 position;
        public float time;
    }

    private void Awake()
    {
        playerActions = new InputSystem_Actions();
        moveAction = playerActions.Player.Move;
        jumpAction = playerActions.Player.Jump;
        rewindAction = playerActions.Player.Rewind;

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

        rewindGhost = Instantiate(rewindPrefab).transform; // Instantiate the rewind ghost prefab
    }

    // Update is called once per frame
    void Update()
    {

        // Record current position with timestamp
        positionHistory.Add(new PositionRecord
        {
            position = transform.position,
            time = Time.time
        });

        // Clean up old records
        positionHistory.RemoveAll(record => Time.time - record.time > rewindTime);

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

        if (rewindAction.triggered && Time.time >= lastRewindTime + rewindCooldown)
        {
            Rewind();
            lastRewindTime = Time.time;
        }

        UpdateRewindGhost();
      

        // Debug cooldown info
        float rewindCooldownRemaining = Mathf.Max(0, (lastRewindTime + rewindCooldown) - Time.time);
        Debug.Log($"[Rewind Cooldown] Remaining: {rewindCooldownRemaining:F2}s");

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

    void Rewind()
    {
        float targetTime = Time.time - rewindTime;
        PositionRecord rewindRecord = positionHistory[0];

        foreach (var record in positionHistory)
        {
            if (Mathf.Abs(record.time - targetTime) < Mathf.Abs(rewindRecord.time - targetTime))
            {
                rewindRecord = record;
            }
        }

        transform.position = rewindRecord.position;
        rb.linearVelocity = Vector3.zero;

        // Clear old trail & start fresh
        positionHistory.Clear();
        positionHistory.Add(new PositionRecord { position = transform.position, time = Time.time });
    }


    void UpdateRewindGhost()
    {
        if (positionHistory.Count == 0 || rewindGhost == null) return;

        float targetTime = Time.time - rewindTime;
        PositionRecord closest = positionHistory[0];

        foreach (var record in positionHistory)
        {
            if (Mathf.Abs(record.time - targetTime) < Mathf.Abs(closest.time - targetTime))
            {
                closest = record;
            }
        }

        rewindGhost.position = closest.position;
    }




}
