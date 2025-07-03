using UnityEngine;

public class DoorTeleporter : InteractableObject
{
    public Transform targetDoor;
    public float exitOffset = 1f; // Offset from target door to place the player
    public bool preserveFacingDirection = true;
    public DoorControl doorControl; // Assign in Inspector or via GetComponentInParent

  

    public override void OnInteract()
    {
        if (doorControl != null && !doorControl.IsOpen)
        {
            Debug.Log("Door is closed. Can't teleport.");
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && targetDoor != null)
        {
            Vector3 offset = targetDoor.right * exitOffset;
            Vector3 newPosition = targetDoor.position + offset;
            newPosition.z = -0.22f; // Lock player to Z=0

            Debug.Log($"Teleporting to: {targetDoor.position + offset}");
            player.transform.position = newPosition;


            if (preserveFacingDirection)
            {
                player.transform.rotation = targetDoor.rotation;
            }

            Debug.Log("Teleported player to " + targetDoor.name);
        }
    }

}
