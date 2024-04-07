using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private TouchControls touchControls;
    void Awake()
    {
        touchControls = new TouchControls();
    }

    private void OnEnable()
    {
        touchControls.Enable(); 
    }
    private void OnDisable()
    {
        touchControls.Disable();        
    }
    private void Start()
    {
        touchControls.Touch.TouchInput.started += context => StartTouch(context);
        touchControls.Touch.TouchInput.canceled += context => EndTouch(context);
    }
    private void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch started " + touchControls.Touch.TouchInput);
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch ended " + touchControls.Touch.TouchInput);
    }

}
