using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SynopsisControler : MonoBehaviour
{

    private InputAction confirmAction;
    public InputSystem_Actions UIControl;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        UIControl = new InputSystem_Actions();
        UIControl.UI.Enable();
        UIControl.Player.Disable();
        confirmAction = UIControl.UI.Confirm;
    }

    // Update is called once per frame
    void Update()
    {
        if (confirmAction.triggered)
        {
            SceneManager.LoadScene("Level 1",LoadSceneMode.Single);  
        }

    }



}
