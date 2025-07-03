using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{

    public MonoBehaviour[] switchSources; // Drag all switches here
    private ISwitch[] switches;
    private bool isOpen = false;
    public bool IsOpen => isOpen;


    void Awake()
    {
        switches = new ISwitch[switchSources.Length];
        for (int i = 0; i < switchSources.Length; i++)
        {
            switches[i] = switchSources[i] as ISwitch;
        }
    }


    public void NotifySwitchActivated()
    {
        if (isOpen) return;

        foreach (var sw in switches)
        {
            if (sw == null || !sw.IsActivated)
                return;
        }

        OpenDoor();
    }

    private void OpenDoor()
    {
        isOpen = true;
        //Debug.Log("Door opens — all switches activated.");
        // Add animation, deactivate, or enable logic here
        gameObject.SetActive(false);
    }
}
