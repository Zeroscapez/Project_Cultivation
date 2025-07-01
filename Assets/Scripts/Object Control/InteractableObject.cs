using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Collider))]
public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    private Material originalMaterial;
    public Material highlightMaterial;

    private Renderer objectRenderer;
    private bool isPlayerNearby = false;
   
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }
    }

    public abstract void OnInteract();
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (objectRenderer != null && highlightMaterial != null)
            {
                objectRenderer.material = highlightMaterial;
            }

            Debug.Log("Player entered interaction range of " + gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (objectRenderer != null && originalMaterial != null)
            {
                objectRenderer.material = originalMaterial;
            }

            Debug.Log("Player exited interaction range of " + gameObject.name);
        }
    }
}
