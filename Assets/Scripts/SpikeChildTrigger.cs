using UnityEngine;

public class SpikeChildTrigger : MonoBehaviour
{
    private MovingSpikes parent;

    private void Start()
    {
        parent = GetComponentInParent<MovingSpikes>();
    }

    private void OnTriggerEnter(Collider other)
    {
        parent?.HandleTrigger(other);
    }
}
