using UnityEngine;

public class DoorTeleporter : InteractableObject
{
    public Transform targetDoor;
    public float exitOffset = 1f; // Offset from target door to place the player
    public bool preserveFacingDirection = true;

    public override void OnInteract()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && targetDoor != null)
        {
            Vector3 offset = targetDoor.forward * exitOffset;
            player.transform.position = new Vector3(
                targetDoor.position.x + offset.x,
                targetDoor.position.y + offset.y, 0F    
                
            );

            if (preserveFacingDirection)
            {
                player.transform.rotation = targetDoor.rotation;
            }

            Debug.Log("Teleported player to " + targetDoor.name);
        }
    }
}
