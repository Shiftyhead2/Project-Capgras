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

    private InputAction movementAction;
    private InputAction lookAction;
    private InputAction sprintAction;
    private InputAction crouchAction;
    private InputAction jumpAction;
    private InputAction zoomAction;

    private InputAction enterDetectiveModeAction;
    private InputAction exitDetectiveModeAction;

    #region PlayerInputActions
    private const string MOVEMENT_ACTION = "Movement";
    private const string MOUSE_LOOK_ACTION = "Look";
    private const string SPRINT_ACTION = "Sprint";
    private const string PLAYER_MOVE_ACTION_MAP = "OnFoot";
    private const string CROUCH_ACTION = "Crouch";
    private const string JUMP_ACTION = "Jump";
    private const string ZOOM_ACTION = "Zoom";
    #endregion


    #region DetectiveModeActions
    private const string DETECTIVE_MODE_ACTION_MAP = "DetectiveMode";
    private const string ENTER_DETECTIVE_MODE_ACTION = "Enter detective mode";
    private const string EXIT_DETECTIVE_MODE_ACTION = "Exit computer";
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        movementAction = playerInput.actions[MOVEMENT_ACTION];
        lookAction = playerInput.actions[MOUSE_LOOK_ACTION];
        sprintAction = playerInput.actions[SPRINT_ACTION];
        crouchAction = playerInput.actions[CROUCH_ACTION];
        jumpAction = playerInput.actions[JUMP_ACTION];
        zoomAction = playerInput.actions[ZOOM_ACTION];


        enterDetectiveModeAction = playerInput.actions[ENTER_DETECTIVE_MODE_ACTION];
        exitDetectiveModeAction = playerInput.actions[EXIT_DETECTIVE_MODE_ACTION];

    }

    private void Update()
    {
        checkSprint();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(movementAction.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(lookAction.ReadValue<Vector2>());
    }


    private void OnEnable()
    {
        crouchAction.performed += handleCrouch;
        jumpAction.performed += handleJump;
        zoomAction.performed += handleZoom;

        enterDetectiveModeAction.performed += handleDetectiveMode;
        exitDetectiveModeAction.performed += handleExitDetectiveMode;

        GameEvents.onComputerInteraction += SwitchToDetectiveInput;
        GameEvents.onDisablePlayerInput += DisableCurrentInput;
        GameEvents.onEnablePlayerInput += SwitchToPlayerInput;
        GameEvents.onComputerShutDown += SwitchToPlayerInput;
        SwitchToPlayerInput();
    }

    private void OnDisable()
    {
        crouchAction.performed -= handleCrouch;
        jumpAction.performed -= handleJump;
        zoomAction.performed -= handleZoom;

        enterDetectiveModeAction.performed -= handleDetectiveMode;
        exitDetectiveModeAction.performed -= handleExitDetectiveMode;

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
        if (sprintAction.IsPressed())
        {
            motor.StartSprint();
        }
        else
        {
            motor.StopSprint();
        }
    }

    void handleJump(InputAction.CallbackContext context)
    {
       motor.Jump();  
    }

    void handleCrouch(InputAction.CallbackContext context)
    {
        motor.Crouch();
    }

    void handleZoom(InputAction.CallbackContext context)
    {
        look.HandleZoom(); 
    }

    void handleDetectiveMode(InputAction.CallbackContext context)
    {
        GameEvents.onEnterDetectiveMode?.Invoke();
    }

    void handleExitDetectiveMode(InputAction.CallbackContext context)
    {
       GameEvents.onExitComputerPressed?.Invoke();  
    }
}
