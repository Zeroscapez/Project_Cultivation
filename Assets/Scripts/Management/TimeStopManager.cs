using System.Collections.Generic;
using UnityEngine;

public class TimeStopManager : MonoBehaviour
{
    public static TimeStopManager Instance { get; private set; }

    private List<ITimeStoppable> stoppables = new List<ITimeStoppable>();
    private bool isTimeStopped = false;

    [Header("Time Stop Settings")]
    public float timeStopDuration = 3f;
    public float timeStopCooldown = 5f;
    private float timeStopEndTime;
    private float lastTimeStopTime;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    public void Register(ITimeStoppable stoppable)
    {
        if (!stoppables.Contains(stoppable))
            stoppables.Add(stoppable);
    }

    public void Unregister(ITimeStoppable stoppable)
    {
        stoppables.Remove(stoppable);
    }

    public void TryToggleTimeStop()
    {
        if (isTimeStopped || Time.time >= lastTimeStopTime + timeStopCooldown)
        {
            ToggleTimeStop();
        }
    }

    private void Update()
    {
        if (isTimeStopped && Time.time >= timeStopEndTime)
        {
            ToggleTimeStop();
        }
    }

    public void ToggleTimeStop()
    {
        isTimeStopped = !isTimeStopped;

        foreach (var obj in stoppables)
        {
            if (isTimeStopped) obj.OnTimeStop();
            else obj.OnTimeResume();
        }

        if (isTimeStopped)
        {
            lastTimeStopTime = Time.time;
            timeStopEndTime = Time.time + timeStopDuration;
        }
    }

    public bool IsTimeStopped => isTimeStopped;
}
