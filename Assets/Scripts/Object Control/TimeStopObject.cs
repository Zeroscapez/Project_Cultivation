using UnityEngine;

public class TimeStopObject : MonoBehaviour, ITimeStoppable
{
    public Rigidbody rb;
   // private Animator animator;
    private Vector3 savedVelocity;
    [SerializeField] private bool isStopped = false;

    private void OnEnable()
    {
        TimeStopManager.Instance?.Register(this);
    }

    private void OnDisable()
    {
        TimeStopManager.Instance?.Unregister(this);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       // animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTimeResume()
    {
        if (!isStopped) return;

        rb.isKinematic = false;
        rb.linearVelocity = savedVelocity;

        //if (animator) animator.speed = 1;
        isStopped = false;
    }

    public void OnTimeStop()
    {
        if (isStopped) return;

        savedVelocity = rb.linearVelocity;
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;

        //if (animator) animator.speed = 0;
        isStopped = true;
    }
}
