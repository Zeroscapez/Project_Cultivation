using UnityEngine;
using UnityEngine.Pool;

public class DoorSwitchShoot : ShootSwitchControl, ISwitch
{
   

    public bool IsActivated => activated;

   

    public override void OnHit()
    {
        Debug.Log("Door Switch Shot");
    }
}
