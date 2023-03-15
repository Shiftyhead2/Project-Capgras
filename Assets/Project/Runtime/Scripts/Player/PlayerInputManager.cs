using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerActions playerInput;
    public PlayerActions.OnFootActions onFoot { get; private set; }
    private PlayerActions.DetectiveModeActions detectiveModeActions;

    private PlayerMotor motor;
    private PlayerLook look;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerActions();
        onFoot = playerInput.OnFoot;
        detectiveModeActions = playerInput.DetectiveMode;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Zoom.performed += ctx => look.HandleZoom();
        detectiveModeActions.Enterdetectivemode.performed += ctx => GameEvents.onEnterDetectiveMode?.Invoke();
    }

    private void Update()
    {
        checkSprint();
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
        GameEvents.onComputerInteraction += DisablePlayerInput;
        GameEvents.onComputerInteraction += EnableDetectiveInput;
        GameEvents.onNPCFullyChecked += EnablePlayerInput;
        GameEvents.onNPCFullyChecked += DisableDetectiveInput;
        GameEvents.onCitationGiven += DisablePlayerInput;
        GameEvents.onCitationModalClosed += EnablePlayerInput;
        GameEvents.onDisablePlayerInput += DisablePlayerInput;
        GameEvents.onEnablePlayerInput += EnablePlayerInput;
        EnablePlayerInput();
        DisableDetectiveInput();
    }

    private void OnDisable()
    {
        GameEvents.onComputerInteraction -= DisablePlayerInput;
        GameEvents.onComputerInteraction -= EnableDetectiveInput;
        GameEvents.onNPCFullyChecked -= EnablePlayerInput;
        GameEvents.onNPCFullyChecked -= DisableDetectiveInput;
        GameEvents.onCitationGiven -= DisablePlayerInput;
        GameEvents.onCitationModalClosed -= EnablePlayerInput;
        GameEvents.onDisablePlayerInput -= DisablePlayerInput;
        GameEvents.onEnablePlayerInput -= EnablePlayerInput;
        GenericDisable();
    }

    void GenericDisable()
    {
        onFoot.Disable();
        detectiveModeActions.Disable();
    }

    private void DisablePlayerInput()
    {
        onFoot.Disable();
    }

    private void EnablePlayerInput()
    {
        onFoot.Enable();
    }

    private void EnableDetectiveInput()
    {
        detectiveModeActions.Enable();
    }

    private void DisableDetectiveInput()
    {
        detectiveModeActions.Disable();
    }

    void checkSprint()
    {
        if (onFoot.Sprint.IsPressed())
        {
            motor.StartSprint();
        }
        else
        {
            motor.StopSprint();
        }
    }
}
