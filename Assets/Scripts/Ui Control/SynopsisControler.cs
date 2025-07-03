using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SynopsisControler : MonoBehaviour
{

    private InputAction confirmAction;
    public InputSystem_Actions UIControl;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIControl = new InputSystem_Actions();
        confirmAction = UIControl.UI.Confirm;
    }

    // Update is called once per frame
    void Update()
    {
        if (confirmAction.triggered)
        {
            Debug.Log("Pressed left-click.");
        }

    }



}
