using UnityEngine;

public class WorldSlowdownManager : MonoBehaviour
{
    public static WorldSlowdownManager Instance;

    public float slowFactor = 0.2f;
    public float slowdownLength = 3f;
    public float cooldown = 5f;

    private float slowEndTime;
    private float lastSlowTime = Mathf.NegativeInfinity;
    private bool isSlowing = false;

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

        UIController.Instance.restrictFastForward.SetActive(isSlowing);
    }

    public void TriggerSlowdown()
    {
        if (isSlowing || Time.unscaledTime < lastSlowTime + cooldown) return;

        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        slowEndTime = Time.unscaledTime + slowdownLength;
        lastSlowTime = Time.unscaledTime;
        isSlowing = true;
    }

    private void ResetTime()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        isSlowing = false;
    }

    public bool IsSlowing => isSlowing;
}
