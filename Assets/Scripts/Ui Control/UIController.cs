using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    public float health;
    public float maxHealth;
    public int ammo;
    public int maxAmmo;

    public bool canRewind;
    public bool canPause;
    public bool canFastFoward;
   

    public Image hpBar;
    public TextMeshProUGUI ammoCount;
    public TextMeshProUGUI rewindCooldown;
    public TextMeshProUGUI pauseCooldown;
    public TextMeshProUGUI fastForwardCooldown;
    public GameObject restrictRewind;
    public GameObject restrictPause;
    public GameObject restrictFastForward;
    public GameObject pauseMenu;
    public InputSystem_Actions UIControl;
    public InputSystem_Actions PlayerControl;

    public static bool IsPaused { get; private set; } = false;


    void Awake()
    {

        // Singleton pattern to ensure only one instance of UIController exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        // Initialize the Input System Actions
        UIControl = new InputSystem_Actions();
        UIControl.UI.Disable();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHealth = 0.3f;
        health = maxHealth;
        maxAmmo = 5;
        ammo = maxAmmo;
        pauseMenu.SetActive(false);
        restrictRewind.SetActive(false);
        restrictPause.SetActive(false);
        restrictFastForward.SetActive(false);
       

    }

    // Update is called once per frame
    void Update()
    {
        ammoCount.SetText(ammo.ToString());     // Updates ammo count to match internal value
        hpBar.fillAmount = health;              // Updates health bar to match internal value
        if (health <= 0)                        // Prevents health from being negative
        {
            health = 0;
        }

        if (health > maxHealth)                        // Prevents health from going over the maximum value
        {
            health = maxHealth;
        }

        if (ammo <= 0)                        // Prevents ammo from being negative
        {
            ammo = 0;
        }

        if (ammo > maxAmmo)                        // Prevents ammo from going over the maximum value
        {
            ammo = maxAmmo;
        }
    }

    public void TakeDamage(float damage)        // Drops health value when taking damage
    {
        damage *= 0.1f;

        if (health > 0)
        {
            health -= damage;
            Debug.Log("Health: " + health);
            Debug.Log(damage);
        }

        if (health <= 0)
        {
            GameOver();
        }
    }

    public void HealHealth(float healing)       // Restores health 
    {
        if ((health != maxHealth) && (health > 0))
        {
            health += healing;
        }
    }

    public void SpendAmmo(int ammoSpent)        // Spends ammo
    {
        if (ammo > 0)
        {
            ammo -= ammoSpent;
        }
    }

    public void Reload()                        // Reloads weapon to maximum value
    {
        ammo = maxAmmo;
    }

    public void PickupAmmo(int ammoGained)      // Reloads weapon by limited value
    {
        if (ammo != maxAmmo)
        {
            ammo += ammoGained;
        }
    }

    public void GameOver()                      // Enables game over screen
    {
        // add game over screen here
    }

    public void PauseGame()
    {
        Debug.Log("Pause Game Triggered");

        if (IsPaused)
        {
            IsPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            
        }
        else
        {
            IsPaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            
        }

            
    }


    public void EnablePause()
    {
  
        if (TimeStopManager.Instance.IsTimeStopped)
        {
            restrictPause.SetActive(true);
        }
        else
        {
            restrictPause.SetActive(false);
            //pause on
        }
    }


    public void EnableFastForward()
    {
        restrictFastForward.SetActive(false);
        //ff on
    }

    public void DisableFastForward()
    {
        restrictFastForward.SetActive(true);
        //ff off
    }


    public void GameEnd()
    {
        Application.Quit();
    }

    public void GoTitle()
    {
        SceneManager.LoadScene("TitleScreen");
    }

}
