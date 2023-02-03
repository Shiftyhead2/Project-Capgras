using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerActions playerInput;
    public PlayerActions.OnFootActions onFoot { get; private set; }

    private PlayerMotor motor;
    private PlayerLook look;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerActions();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Sprint.performed += ctx => motor.Sprint();
        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Zoom.performed += ctx => look.HandleZoom();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }


    private void OnEnable()
    {
        GameEvents.onComputerInteraction += DisableInput;
        EnableInput();
    }

    private void OnDisable()
    {
        GameEvents.onComputerInteraction -= DisableInput;
        DisableInput();
    }

    private void DisableInput()
    {
        onFoot.Disable();
    }

    private void EnableInput()
    {
        onFoot.Enable();
    }
}
