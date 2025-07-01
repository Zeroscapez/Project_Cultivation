using UnityEngine;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;

public class TimeStopManager : MonoBehaviour
{
    public static TimeStopManager Instance { get; private set; }

    private List<ITimeStoppable> stoppables = new List<ITimeStoppable>();
    private bool isTimeStopped = false;

    public float timeStopLength = 5f;

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
        if (stoppables.Contains(stoppable))
            stoppables.Remove(stoppable);
    }

    public void ToggleTimeStop()
    {
        timeStopLength = 5f;
        isTimeStopped = !isTimeStopped;

        foreach (var obj in stoppables)
        {
            if (isTimeStopped)
                obj.OnTimeStop();
            else
                obj.OnTimeResume();
        }
    }
}
