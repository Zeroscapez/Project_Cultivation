using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Renderer), typeof(Collider))]
public abstract class ShootSwitchControl : MonoBehaviour, ITarget
{
    private Material originalMaterial;
    public Material strikeMaterial;

    private Renderer objectRenderer;

    public bool activated = false;

    public DoorControl linkedDoor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public abstract void OnHit();



    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("PAttack"))
        {
            if (!activated)
            {
                activated = true;

                if (objectRenderer != null && strikeMaterial != null)
                {
                    objectRenderer.material = strikeMaterial;
                }

               // Debug.Log("Object Hit with Bullet - " + gameObject.name);

                if (linkedDoor != null)
                {
                    linkedDoor.NotifySwitchActivated();
                }
            }
        }

    }


}
