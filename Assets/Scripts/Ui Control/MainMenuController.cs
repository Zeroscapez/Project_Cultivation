using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MainMenuController : MonoBehaviour
{

    public GameObject titleScreen;
    public GameObject synopsisScreen;
    public GameObject controlsScreen;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        titleScreen.SetActive(true);
        synopsisScreen.SetActive(false);
        controlsScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoSynopsis()
    {
        titleScreen.SetActive(false);
        synopsisScreen.SetActive(true);
        controlsScreen.SetActive(false);
    }

    public void GoSynopsis()
    {
        titleScreen.SetActive(false);
        synopsisScreen.SetActive(true);
        controlsScreen.SetActive(false);
    }

    public void GoControls()
    {
        titleScreen.SetActive(false);
        synopsisScreen.SetActive(false);
        controlsScreen.SetActive(true);
    }

}
