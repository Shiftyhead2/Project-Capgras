using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    private bool ShouldCrouch => !duringCrouchAnimation && isGrounded;
    private bool ShouldJump => !duringCrouchAnimation && isGrounded && !isCrouching;
    private bool ShouldSprint => !duringCrouchAnimation && isGrounded && !isCrouching;

    private CharacterController characterController;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool isSpriting;
    private float speed;
    [Header("Variables")]
    [SerializeField]
    private float walkingSpeed = 5f;
    [SerializeField]
    private float runningSpeed = 8f;
    [SerializeField]
    private float gravity = -9.8f;
    [SerializeField]
    private float jumpHeight = 3f;
    [SerializeField]
    private Camera cam;

    [Header("Crouch Parameters")]
    [SerializeField] float crouchHeight = 0.5f;
    [SerializeField] float standingHeight = 2f;
    [SerializeField] float timeToCrouch = 0.25f;
    [SerializeField] Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] Vector3 standingCenter = new Vector3(0, 0, 0);
    private bool isCrouching;
    private bool duringCrouchAnimation;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        speed = walkingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = characterController.isGrounded;
        if (isCrouching)
        {
            speed = 3.5f;
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
        
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        characterController.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        characterController.Move(playerVelocity * Time.deltaTime);
        //Debug.Log(playerVelocity.y);
    }

    public void Jump()
    {
        if(ShouldJump)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            if (isSpriting)
            {
                isSpriting = false;
            }
        }
    }

    public void Sprint()
    {
        if (ShouldSprint)
        {
            isSpriting = !isSpriting;
        }
    }

    public void Crouch()
    {
        if (ShouldCrouch)
        {
            StartCoroutine(CrouchStand());
            if (isSpriting)
            {
                isSpriting = false;
            }
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
