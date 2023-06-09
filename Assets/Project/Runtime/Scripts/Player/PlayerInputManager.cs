using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The main class that is responsible for managing the player input. This class is actually generic and easy to customize 
/// thanks to the new Unity input system :)
/// </summary>
public class PlayerInputManager : MonoBehaviour
{
    public PlayerInput playerInput { get; private set; }


    private PlayerMotor motor;
    private PlayerLook look;

    private const string MOVEMENT_ACTION = "Movement";
    private const string MOUSE_LOOK_ACTION = "Look";
    private const string SPRINT_ACTION = "Sprint";
    private const string PLAYER_MOVE_ACTION_MAP = "OnFoot";
    private const string DETECTIVE_MODE_ACTION_MAP = "DetectiveMode";

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
    }

    private void Update()
    {
        checkSprint();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(playerInput.actions[MOVEMENT_ACTION].ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(playerInput.actions[MOUSE_LOOK_ACTION].ReadValue<Vector2>());
    }


    private void OnEnable()
    {
        GameEvents.onComputerInteraction += SwitchToDetectiveInput;
        GameEvents.onDisablePlayerInput += DisableCurrentInput;
        GameEvents.onEnablePlayerInput += SwitchToPlayerInput;
        GameEvents.onComputerShutDown += SwitchToPlayerInput;
        SwitchToPlayerInput();
    }

    private void OnDisable()
    {
        GameEvents.onComputerInteraction -= SwitchToDetectiveInput;
        GameEvents.onDisablePlayerInput -= DisableCurrentInput;
        GameEvents.onEnablePlayerInput -= SwitchToPlayerInput;
        GameEvents.onComputerShutDown -= SwitchToPlayerInput;
    }

    void DisableCurrentInput()
    {
        playerInput.currentActionMap.Disable();
    }


    void SwitchToPlayerInput()
    {
        playerInput.currentActionMap.Disable();
        playerInput.SwitchCurrentActionMap(PLAYER_MOVE_ACTION_MAP);
        playerInput.currentActionMap.Enable();
    }

    void SwitchToDetectiveInput()
    {
        playerInput.currentActionMap.Disable();
        playerInput.SwitchCurrentActionMap(DETECTIVE_MODE_ACTION_MAP);
        playerInput.currentActionMap.Enable();
    }

    void checkSprint()
    {
        if (playerInput.actions[SPRINT_ACTION].IsPressed())
        {
            motor.StartSprint();
        }
        else
        {
            motor.StopSprint();
        }
    }

    public void handleJump(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {
            motor.Jump();
        }
    }

    public void handleCrouch(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {
            motor.Crouch();
        }
    }

    public void handleZoom(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {
            look.HandleZoom();
        }
    }

    public void handleDetectiveMode(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {
            GameEvents.onEnterDetectiveMode?.Invoke();
        }
    }

    public void handleExitDetectiveMode(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {
            GameEvents.onExitComputerPressed?.Invoke();
        }
    }
}
