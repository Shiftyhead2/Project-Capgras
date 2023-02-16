using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    private bool ShouldCrouch => !duringCrouchAnimation && isGrounded && canCrouch;
    private bool ShouldJump => !duringCrouchAnimation && isGrounded && !isCrouching && canJump;
    private bool ShouldSprint => !duringCrouchAnimation && isGrounded && !isCrouching && canSprint;

    [Header("Feature toggles")]
    [SerializeField] private bool toggleHeadBob = true;
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool canCrouch = true;
    [SerializeField] private bool canJump = true;

    [Header("Camera")]
    [field:SerializeField]
#if UNITY_EDITOR
    [field: ReadOnlyInspector]
#endif
    private Camera cam;

    private CharacterController characterController;
    private Vector3 playerVelocity;
    Vector3 moveDirection = Vector3.zero;
    private bool isGrounded;
    private bool isSpriting;
    private float speed;
    [Header("Movement Settings")]
    [SerializeField]
    private float walkingSpeed = 5f;
    [SerializeField]
    private float runningSpeed = 8f;
    [SerializeField]
    private float crouchingSpeed = 3.5f;
    [SerializeField]
    private float slopeSpeed = 8f;
    [SerializeField]
    private float gravity = -9.8f;
    [SerializeField]
    private float jumpHeight = 3f;
    
    

    [Header("Crouch Settings")]
    [SerializeField] float crouchHeight = 0.5f;
    [SerializeField] float standingHeight = 2f;
    [SerializeField] float timeToCrouch = 0.25f;
    [SerializeField] Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] Vector3 standingCenter = new Vector3(0, 0, 0);
    private bool isCrouching;
    private bool duringCrouchAnimation;

    [Header("Headbob Settings")]
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float runBobSpeed = 18f;
    [SerializeField] private float runBobAmount = 0.1f;
    [SerializeField] private float crouchBobSpeed = 8f;
    [SerializeField] private float crouchBobAmount = 0.025f;
    private float defaultYPos = 0;
    private float timer;

    //Slope settings;
    private Vector3 hitSlopeNormal;

    private bool IsSliding
    {
        get
        {
            if(isGrounded && Physics.Raycast(transform.position,Vector3.down,out RaycastHit slopeHit, 2f))
            {
                hitSlopeNormal = slopeHit.normal;
                return Vector3.Angle(hitSlopeNormal, Vector3.up) > characterController.slopeLimit;
            }
            else
            {
                return false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cam = GetComponent<PlayerLook>().cam;
        defaultYPos = cam.transform.localPosition.y;
        speed = walkingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = characterController.isGrounded;
        if (isCrouching)
        {
            speed = crouchingSpeed;
        }
        else
        {
            if (isSpriting)
            {
                speed = runningSpeed;
            }
            else
            {
                speed = walkingSpeed;
            }
            
        }

        if (!canSprint || IsSliding || !isGrounded || isCrouching)
        {
            isSpriting = false;
        }

        if (toggleHeadBob)
        {
            HandleHeadBob();
        }

        if (canMove)
        {
            ApplyFinalMovements();
        }
        
    }

    public void ProcessMove(Vector2 input)
    {
        if (!canMove) return;
        
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        
        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y < 0 && !IsSliding)
        {
            playerVelocity.y = -2f;
        }
    }

    void ApplyFinalMovements() 
    {

        if (IsSliding)
        {
            playerVelocity.y = 0f;
            playerVelocity = new Vector3(hitSlopeNormal.x, -hitSlopeNormal.y, hitSlopeNormal.z) * slopeSpeed;
        }
        else
        {
            playerVelocity.x = 0f;
            playerVelocity.z = 0f;
        }

        characterController.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if(ShouldJump)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void StartSprint()
    {

        if (ShouldSprint)
        {
            isSpriting = true;
        }
    }

    public void StopSprint()
    {
        if(ShouldSprint)
        {
            isSpriting = false;
        }
    }

    public void Crouch()
    {
        if (ShouldCrouch)
        {
            StartCoroutine(CrouchStand());
        }
    }

    private void HandleHeadBob()
    {
        if (!isGrounded)  return;
        
        if(Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * (isCrouching ? crouchBobSpeed : isSpriting ? runBobSpeed : walkBobSpeed);
            cam.transform.localPosition = new Vector3(
                cam.transform.localPosition.x,
                defaultYPos + Mathf.Sin(timer) * (isCrouching ? crouchBobAmount : isSpriting ? runBobAmount : walkBobAmount),
                cam.transform.localPosition.z);
        }
    }

    private IEnumerator CrouchStand()
    {

        if(isCrouching && Physics.Raycast(cam.transform.position, Vector3.up, 1f))
        {
            yield break;
        }


        duringCrouchAnimation = true;
        float timeElapsed = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;

        while (timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;

        isCrouching = !isCrouching;

        duringCrouchAnimation = false;
    }
}
