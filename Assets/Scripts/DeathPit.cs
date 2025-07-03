using UnityEngine;

public class DeathPit : MonoBehaviour
{
    public Transform startPoint;
    public Transform sphereSpawn;
    public GameObject pitBalls;
    
    void Start()
    {
        pitBalls = Instantiate(pitBalls, sphereSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 respawnPosition = startPoint.position;
            respawnPosition.z = -0.22f;
            other.gameObject.transform.position = respawnPosition;
        }

        if (other.gameObject.CompareTag("PitBalls"))
        {
            other.gameObject.transform.position = sphereSpawn.position;
            other.gameObject.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, 0);
        }
        
    }
}
