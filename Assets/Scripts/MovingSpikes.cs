using UnityEngine;

public class MovingSpikes : MonoBehaviour
{

    public float interval = 1f;

    private float timer;
    private bool isSpikeUp = false;

    private GameObject spikesUP;


    private void Awake()
    {
        spikesUP = transform.GetChild(0).gameObject; // Assuming the spikes are the first child of this GameObject
    }
    void Start()
    {
        // Automatically find the first child as the spike object
        if (transform.childCount > 0)
        {
            spikesUP = transform.GetChild(0).gameObject;
        }
        else
        {
            Debug.LogWarning("No child object found for spikesUP!");
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0f;
            isSpikeUp = !isSpikeUp;
            spikesUP.SetActive(isSpikeUp);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit by spike!");
            // Add damage logic here (e.g., call a TakeDamage() method)
        }
    }
}
