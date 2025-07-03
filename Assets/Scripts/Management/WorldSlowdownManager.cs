using UnityEngine;

public class WorldSlowdownManager : MonoBehaviour
{
    public static WorldSlowdownManager Instance;

    public float slowFactor = 0.2f;
    public float slowdownLength = 3f;
    public float cooldown = 5f;

    private float slowEndTime;
    public float lastSlowTime = Mathf.NegativeInfinity;
    private bool isSlowing = false;

    public float CooldownRemaining
    {
        get
        {
            // Only start tracking cooldown *after* slow ends
            if (isSlowing) return cooldown;

            float cooldownEnd = lastSlowTime + cooldown;
            float remaining = cooldownEnd - Time.unscaledTime;
            return Mathf.Max(0f, remaining);
        }
    }
    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    void Update()
    {
        if (isSlowing && Time.unscaledTime >= slowEndTime)
        {
            ResetTime();
        }

        UIController.Instance.restrictFastForward.SetActive(isSlowing || CooldownRemaining > 0);


    }



    public void TriggerSlowdown()
    {
        if (isSlowing || Time.unscaledTime < lastSlowTime + cooldown) return;

        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        slowEndTime = Time.unscaledTime + slowdownLength;
        isSlowing = true;
    }


    private void ResetTime()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        isSlowing = false;
        lastSlowTime = Time.unscaledTime; 
    }


    public bool IsSlowing => isSlowing;
}
