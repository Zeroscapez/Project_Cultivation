using UnityEngine;
using UnityEngine.ResourceManagement.Profiling;

public class PlayerBullet : MonoBehaviour
{
    public float lifetime = 5f; // Lifetime of the bullet in seconds
    private float damage = 10f; // Damage dealt by the bullet

    void Start()
    {
        // Destroy the bullet after its lifetime expires
        Destroy(gameObject, lifetime);

        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.CompareTag("Enemy"))
        {

        }
        
           
        Destroy(gameObject); // Destroy the bullet on collision

    }
}
