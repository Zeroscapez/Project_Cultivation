using UnityEngine;
using UnityEngine.Pool;

public class DoorSwitchTouch : InteractableObject, ISwitch
{
    public bool isActivated = false;
    public DoorControl linkedDoor;
    public bool IsActivated => isActivated;

    public AudioSource activationSound;

    private Renderer objectRenderera;

    void Start()
    {
        objectRenderera = this.gameObject.GetComponent<Renderer>();
        activationSound = GetComponent<AudioSource>();
    }
    public override void OnInteract()
    {
        if (!isActivated)
        {
            isActivated = true;
            if (objectRenderera != null && highlightMaterial != null)
            {
                objectRenderera.material = activatedMaterial;
            }

            if (interactIcon != null)
            {
                interactIcon.SetActive(false);
            }
            activationSound?.Play();



            linkedDoor.NotifySwitchActivated();
        }
    }

    
}
