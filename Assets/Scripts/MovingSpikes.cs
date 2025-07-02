using UnityEngine;

public class MovingSpikes : MonoBehaviour
{
    public Transform upPosition;
    public Transform downPosition;

    public float moveSpeed = 2f;
    public float waitTime = 1f;

    private Transform targetPosition;
    private float waitTimer = 0f;
    private bool isWaiting = false;

    void Start()
    {
        targetPosition = upPosition;
    }

    void Update()
    {
        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                waitTimer = 0f;
                isWaiting = false;
                targetPosition = (targetPosition == upPosition) ? downPosition : upPosition;
            }
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition.position) < 0.01f)
        {
            isWaiting = true;
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
