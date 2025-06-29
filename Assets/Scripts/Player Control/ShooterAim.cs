using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShooterAim : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform fireOrigin;
    public float projectileSpeed = 20f;

    private InputSystem_Actions playerActions;
    private InputAction mousePos;
    private InputAction shootAction;
    private InputAction mouseMove;

    private Vector2 mouseLook;
    private Vector2 aimDirection;

    public float MouseResetTime = 6f;
    private Vector2 MouseOrigin;

    public Transform pointer;
    public float maxAimRadius = 5f; // Maximum distance from the player to aim

    private void Awake()
    {
        playerActions = new InputSystem_Actions();
        MouseOrigin = Camera.main.WorldToScreenPoint(this.transform.position);
    }

    private void OnEnable()
    {
        playerActions.Player.Enable();
        mousePos = playerActions.Player.Aim;
        shootAction = playerActions.Player.Attack;
        mouseMove = playerActions.Player.Look;
    }

    private void OnDisable()
    {
        playerActions.Player.Disable();
    }

    private void Update()
    {
        aimDirection = mousePos.ReadValue<Vector2>();
        mouseLook = mouseMove.ReadValue<Vector2>();
        

        if (shootAction.triggered)
        {
            Shoot();
        }

        if (mouseLook.x > 0 || mouseLook.x < 0) // If mouse is moved
        {
            Aim(); // Aim the player towards the mouse position
            UpdateCursorPosition(); 
            MouseResetTime = 6f; // Reset the timer when mouse is moved

        }
        else if(mouseLook.x == 0) // If mouse is not moved
        {
            MouseResetTime -= Time.deltaTime;
            Debug.Log("Mouse Reset Time: " + MouseResetTime);

            if(MouseResetTime <= 0 && mouseLook.x == 0)
            {
                MouseResetTime = 6f;
                aimDirection = MouseOrigin;
                UpdateCursorPosition();
                Debug.Log("Mouse Reset to Origin");
            }

        }
       

        

    }

    private void Shoot() // Handle shooting logic
    {
        Ray ray = Camera.main.ScreenPointToRay(aimDirection);
        Plane shootPlane = new Plane(Vector3.forward, 0); // Adjust to match your character plane

        if (shootPlane.Raycast(ray, out float enter))
        {
            Vector3 targetPoint = ray.GetPoint(enter);
            Vector3 direction = (pointer.position - fireOrigin.position).normalized;

            GameObject bullet = Instantiate(projectilePrefab, fireOrigin.position, Quaternion.LookRotation(direction));
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.linearVelocity = direction * projectileSpeed;
            Debug.DrawRay(fireOrigin.position, direction * 5f, Color.red, 2f);


        }


    }

    private void Aim() // Aim the player towards the mouse position
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(
         new Vector3(aimDirection.x, aimDirection.y, Camera.main.transform.position.z * -1f)
     );


        if (mouseWorldPos.x < this.transform.position.x)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }

        Debug.Log(mouseWorldPos);
    }


    private void UpdateCursorPosition() // Update the cursor position based on mouse movement
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(
     new Vector3(aimDirection.x, aimDirection.y, Camera.main.transform.position.z * -1f)
 );

        worldPos.z = 0; // Lock it to the same plane as the player

        Vector3 fromPlayerToMouse = worldPos - transform.position;

        if (fromPlayerToMouse.magnitude > maxAimRadius)
        {
            fromPlayerToMouse = fromPlayerToMouse.normalized * maxAimRadius;
        }

        Vector3 clampedWorldPos = transform.position + fromPlayerToMouse;
        pointer.position = clampedWorldPos;
    }
}
