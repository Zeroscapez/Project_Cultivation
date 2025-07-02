using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 2f;
    public float waitTime = 2f;

    public bool isMoving = false;
    private bool goingUp = true;
    private float waitTimer = 0f;

    void Update()
    {
        if (!isMoving) return;

        Transform target = goingUp ? endPoint : startPoint;
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                waitTimer = 0f;
                goingUp = !goingUp;
                isMoving = false; // Stop until triggered again
            }
        }
    }

    public void ActivateElevator()
    {
        isMoving = true;
    }
}
