
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Set the player FBX as a Child of a Emtpy Object Call it Player Holder
    // Set the Peramiters for the Character animator
    // Make sure the cameraT is the has the Main Cam tag
    // Add pivot in Player Holder set it ubove head of player

    public string MoveAnimName = "Forward";
    public string JumpAnimName = "Jumping";
    public string DoubleJumpName = "DoubleJumping";
    public float walkSpeed = 2;
    public float runSpeed = 6;
    public float gravity = -12;
    public float jumpHeight = 1;
    [Range(0, 1)]
    public float airControlPercent;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    float velocityY;

    Animator animator;
    Transform cameraT;
    CharacterController controller;

    public Vector3 velocity;

    // Knock Back
    public float KnockBackForce = 3f;
    public float knockBackTime = 0.25f;
    private float KnockBackCounter;
    public bool canMove;


    // Double Jump
    public int extraJumps;
    public int ExtrajumpCount = 2;

    public ParticleSystem dust;
    public ParticleSystem LandDust;
    public bool landed;
    void Start()
    {
        //    animator = GetComponent<Animator>();
        animator = GetComponentInChildren<Animator>();
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        canMove = true;
        ExtrajumpCount = 0;
    }

    void Update()
    {

        
        if (canMove == false)
        {
            velocityY += Time.deltaTime * gravity;
            velocity += Vector3.up * velocityY;

            controller.Move(velocity * Time.deltaTime);
            return;
        }
            
        // make sure player is not being knock back
        if (KnockBackCounter <= 0)
        {


            // input
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 inputDir = input.normalized;
            bool running = Input.GetKey(KeyCode.LeftShift);

            Move(inputDir, running);

            if (Input.GetKeyDown(KeyCode.Space) )
            {
                Jump();
            }
            if (controller.isGrounded)
            {
                ExtrajumpCount = 0;
            }
            // animator
            float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
            animator.SetFloat(MoveAnimName, animationSpeedPercent, speedSmoothTime, Time.deltaTime);
        }
        else
        {
            KnockBackCounter -= Time.deltaTime;

            velocityY += Time.deltaTime * gravity;
            velocity  += Vector3.up * velocityY;

            controller.Move(velocity * Time.deltaTime);
        }
    }

    void Move(Vector2 inputDir, bool running)
    {
        if (inputDir != Vector2.zero)
        {
           
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
        }

        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

        if (running && controller.isGrounded == true)
        {
            CreateDust();
        }
        velocityY += Time.deltaTime * gravity;
       // Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;
         velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        if (controller.isGrounded)
        {
            velocityY = 0;
            animator.SetBool(JumpAnimName, false);
            if( landed == false)
            {
                landed = true;
                CreateJumpDust();
            }
        }

    }

    void Jump()
    {
        if (controller.isGrounded )
        {
            landed = false;
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
            animator.SetBool(JumpAnimName, true);
           
        }
        else if(controller.isGrounded == false && ExtrajumpCount < extraJumps)
        {
            ExtrajumpCount += 1;
            InAirJump();
        }

    }

   public void InAirJump()
    {
        if (controller.isGrounded == false)
        {
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
            animator.SetTrigger(DoubleJumpName);
            CreateJumpDust();

        }

    }

    float GetModifiedSmoothTime(float smoothTime)
    {
        if (controller.isGrounded)
        {
            return smoothTime;
        }

        if (airControlPercent == 0)
        {
            return float.MaxValue;
        }
        return smoothTime / airControlPercent;
    }


    public void KnockBack(Vector3 direction)
    {
        KnockBackCounter = knockBackTime;
        velocity = direction * KnockBackForce;
        velocity.y = KnockBackForce;
        
    }

    void CreateDust()
    {
        dust.Play();
    }

    void CreateJumpDust()
    {
        LandDust.Play();
    }
}
