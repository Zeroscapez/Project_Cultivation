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
    private float lastTimeStopTime = Mathf.NegativeInfinity;

    public float CooldownRemaining
    {
        get
        {
            if (isTimeStopped) return timeStopCooldown;
            float cooldownEnd = lastTimeStopTime + timeStopCooldown;
            return Mathf.Max(0f, cooldownEnd - Time.time);
        }
    }
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
        if (isTimeStopped || CooldownRemaining <= 0f)
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

        UIController.Instance.restrictPause.SetActive(isTimeStopped || CooldownRemaining > 0);
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
            timeStopEndTime = Time.time + timeStopDuration;
        }
        else
        {
            lastTimeStopTime = Time.time; // cooldown starts when time resumes
        }
    }

  



    public bool IsTimeStopped => isTimeStopped;
}
