using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{

    public float health;
    public float maxHealth;
    public int ammo;
    public int maxAmmo;

    public bool canRewind;
    public bool canPause;
    public bool canFastFoward;

    public Image hpBar;
    public TextMeshProUGUI ammoCount;
    public GameObject restrictRewind;
    public GameObject restrictPause;
    public GameObject restrictFastForward;
    public GameObject pauseMenu;



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
        if (health > 0)
        {
            health -= damage;
        }

        if (health == 0)
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
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void EnableRewind()
    {
        restrictRewind.SetActive(false);
        //rewind on
    }

    public void DisableRewind()
    {
        restrictRewind.SetActive(true);
        //rewind off
    }

    public void EnablePause()
    {
        restrictPause.SetActive(false);
        //pause on
    }

    public void DisablePause()
    {
        restrictPause.SetActive(true);
        //pause off
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
}
