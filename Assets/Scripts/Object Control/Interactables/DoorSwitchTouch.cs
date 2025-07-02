using UnityEngine;
using UnityEngine.Pool;

public class DoorSwitchTouch : InteractableObject, ISwitch
{
    public bool isActivated = false;
    public DoorControl linkedDoor;
    public bool IsActivated => isActivated;
    public override void OnInteract()
    {
        if (!isActivated)
        {
            isActivated = true;
            linkedDoor.NotifySwitchActivated();
        }
    }
}
