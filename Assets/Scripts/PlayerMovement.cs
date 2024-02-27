using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public enum PlayerMovementState
    {
        Flying,
        Grounded
    };

    [HideInInspector]
    public PlayerMovementState playerMovementState = PlayerMovementState.Flying;

    [SerializeField]
    private GameObject cameraTarget;

    [SerializeField]
    private float rotSpeed = 1f;

    [Header("Camera")]
    [SerializeField]
    private float camRotSpeed = 1f;
    [SerializeField]
    private float camRotIncrement = 5f;
    [SerializeField]
    private float maxCameraXRot = 45f;
    [SerializeField]
    private float mouseDeltaDeadZone = 0.1f;
    

    private CustomInput customInput = null;
    private CharacterController characterController;

    private Quaternion inputRotation = Quaternion.identity;
    private Quaternion cameraTargetRotation = Quaternion.identity;


    [Header("Grounded")]
    [SerializeField]
    private float groundSpeed = 5f;
    [SerializeField]
    private float groundGravity = -10f;
    [SerializeField]
    private float takeoffSpeed = 5f;
    [SerializeField]
    private float groundedCheckTraceLength = 2f;
    [SerializeField]
    private LayerMask groundedCheckLayerMask;

    [Header("Flight")]
    [SerializeField]
    private float maxFlySpeed = 5f;
    [SerializeField]
    private float minFlySpeed = 1f;


    [SerializeField]
    private float glideYVelocity = -3f;
    [SerializeField]
    private float diveAcceleration = -30;
    [SerializeField]
    private float diveSreakBuilder = 1f;
    [SerializeField]
    private float maxRiseSpeed = 8f;
    [SerializeField]
    private float gravity = -9.81f;
    [SerializeField]
    private float fallTime = 5f;
    [SerializeField,Range(0,90)]
    private float groundAngle = 45f;


    private float currentYSpeed = 0;
    private float currentFlySpeed = 0;
    private bool isDiving = false;
    private bool isFalling = false;
    private float fallTimer = 0;
    private Vector3 hitNormal;
    


    private RhythmTracker rhythmTracker; 


    private Vector2 prevMousePos = Vector2.zero;
    private Vector2 curMousePos = Vector2.zero;
    


    //Get bird prefab
    public GameObject birdPrefab;

    //Get bird animator
    Animator birdAnimator;


    private Color debugGroundedColor = Color.blue;

    private void Awake()
    {
        customInput = new CustomInput();
        characterController = GetComponent<CharacterController>();
        birdAnimator = birdPrefab.GetComponent<Animator>();
        rhythmTracker = GetComponent<RhythmTracker>();
    }

    private void OnEnable()
    {
        customInput.Enable();
    }

    private void OnDisable()
    {
        customInput.Disable();

    }

    private void Start()
    {
        prevMousePos = Mouse.current.position.ReadValue();
    }
    private void Update()
    {
        CheckIfGrounded();

        HandleCamera();
        switch (playerMovementState)
        {
            case PlayerMovementState.Grounded:
            {
                HandleGroundMovement();
                    birdAnimator.SetTrigger("GroundIdle");
                break;
            }
            case PlayerMovementState.Flying :
            {
                HandleRhythm();
                HandleRotation();
                Movement();
                break;
            }
        }
    }
   
    // Raycasting down to check if hitting ground
    private void CheckIfGrounded()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        bool hit = Physics.Raycast(ray, groundedCheckTraceLength);
        //Debug.DrawLine(ray.origin, ray.origin + ray.direction * groundedCheckTraceLength, Color.red, groundedCheckLayerMask.value);
        if (hit)
        {
            debugGroundedColor = Color.magenta;
            playerMovementState = PlayerMovementState.Grounded;
        }
        else 
        {
            debugGroundedColor = Color.blue;
            playerMovementState = PlayerMovementState.Flying;

        }
    }

    // Handles bird movement while in grounded state.
    private void HandleGroundMovement()
    {
        // adding gravity
        float gravSpeed = 0;
        if(!characterController.isGrounded)
        {
            gravSpeed = groundGravity * Time.deltaTime;
        }


        // player lateral input
        Vector3 moveVector = new Vector3(0, gravSpeed, 0);
        Vector2 val = customInput.Grounded.Movement.ReadValue<Vector2>();

        if(customInput.Grounded.Movement.IsPressed())
        {
            moveVector += transform.forward * val.y * groundSpeed;
            moveVector += transform.right * val.x * groundSpeed;

            birdAnimator.SetTrigger("Walk");
            //Debug.Log("walking");
        }

        // Adding take-of velocity
        if(customInput.Grounded.TakeOff.IsPressed())
        {
            moveVector.y += takeoffSpeed;
        }

        moveVector *= Time.deltaTime;


        characterController.Move(moveVector);
    }

    // Handles rotating the player.
    private void HandleRotation()
    {
        //if (!customInput.Gameplay.Turning.IsPressed()) return;

        float val = Mathf.Sign(customInput.Gameplay.Turning.ReadValue<float>());
        //Debug.Log(val);

        if(customInput.Gameplay.Turning.IsPressed())
        {
            birdAnimator.SetFloat("flyingDirectionX", val);
            birdAnimator.SetBool("flying", false);
        }
        else
        {
            birdAnimator.SetBool("flying", true);
            return;
        }

        inputRotation = Quaternion.Euler(0, val * rotSpeed * Time.deltaTime, 0);

        // Player Rotation
        transform.rotation *= inputRotation;
        inputRotation = Quaternion.identity;
    }
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
        if (Vector3.Angle(hit.normal, Vector3.up) > groundAngle && !isFalling)
        {
            isFalling = true;
            rhythmTracker.ResetStreak();
            Debug.Log("Hit Something!");
        }
    }

    private void HandleRhythm()
    {
        isDiving = false;
        rhythmTracker.ProtectStreak = false;//only true when diving
        if (isFalling)
        {
            if (rhythmTracker.IsFlapping&& rhythmTracker.IsSuccess)//only stop falling if building a streak
            {
                currentYSpeed = 0;
            }
            else
            {
                currentYSpeed += gravity * Time.deltaTime;
                currentYSpeed = Mathf.Clamp(currentYSpeed, -100, glideYVelocity);
            }
            fallTimer += Time.deltaTime;
            if (fallTimer > fallTime)
            {
                isFalling = false;
                fallTimer = 0;
            }
            Debug.Log("Falling!");
        }
        else
        {
            if (customInput.Gameplay.Dive.IsPressed())
            {
                currentYSpeed += diveAcceleration * Time.deltaTime;
                currentYSpeed = Mathf.Clamp(currentYSpeed, -100, glideYVelocity);
                rhythmTracker.ProtectStreak = true;
                isDiving = true;
                birdAnimator.SetBool("Dive",true);
                
            }
            else if (rhythmTracker.IsFlapping)
            {
                currentYSpeed = maxRiseSpeed * rhythmTracker.Accuracy;
                currentYSpeed = Mathf.Clamp(currentYSpeed, -glideYVelocity, maxRiseSpeed);
                birdAnimator.SetBool("Dive", false);
            }
            else
            {
                currentYSpeed = Mathf.Clamp(currentYSpeed, -100, glideYVelocity);
                birdAnimator.SetBool("Dive", false);
            }
            currentFlySpeed = Mathf.Lerp(minFlySpeed, maxFlySpeed, rhythmTracker.Accuracy);
            currentFlySpeed = Mathf.Clamp(currentFlySpeed, 0, maxFlySpeed);
        }

        if (GetComponent<CharacterController>().isGrounded)
        {
            currentFlySpeed = 0;
            Debug.Log("Grounded!");
        }

        
     
    }


    // Camera revolves with mouse move if LMB is held down
    private void HandleCamera()
    {

        curMousePos = Mouse.current.position.ReadValue();
        Vector2 mouseDelta = curMousePos - prevMousePos;
        prevMousePos = curMousePos;

        if (!Mouse.current.leftButton.isPressed) return;

        // Zeroing out if in dead zone
        if (Mathf.Abs(mouseDelta.x) <= mouseDeltaDeadZone) mouseDelta.x = 0;
        if (Mathf.Abs(mouseDelta.y) <= mouseDeltaDeadZone) mouseDelta.y = 0;

        //val.Normalize();

        cameraTargetRotation =  Quaternion.AngleAxis(-mouseDelta.x * camRotSpeed * Time.deltaTime, Vector3.up) * 
                                Quaternion.AngleAxis(-mouseDelta.y * camRotSpeed * Time.deltaTime, Vector3.right);

        // Camera Rotation
        cameraTarget.transform.rotation = cameraTargetRotation * cameraTarget.transform.rotation;
        cameraTargetRotation = Quaternion.identity;

        // Clamping rotation on x axis to prevent camera from going upside-down
        var angles = cameraTarget.transform.localEulerAngles;
        if (angles.x > 180)
        {
            angles.x = Mathf.Clamp(angles.x, 360 - maxCameraXRot, 360);
        }
        else
        {
            angles.x = Mathf.Clamp(angles.x, 0, maxCameraXRot);
        }

        cameraTarget.transform.localEulerAngles = angles;

        
    }


    private void Movement()
    {
        Vector3 movement;
        if (!isFalling)
        {
            movement = transform.forward * currentFlySpeed;
            
        }
        else
        {
            movement = hitNormal * currentFlySpeed;
            
        }
        movement.y = currentYSpeed;
        if (isDiving)
        {
            //Make it more forgiving if a player wants to try to graze a cliff
            if(Physics.SphereCast(transform.position,characterController.radius,Vector3.down,out RaycastHit hit ,Mathf.Abs(movement.y) * Time.deltaTime+0.5f, 1<<LayerMask.NameToLayer("Default")))
            {
                Vector3 skimDirection = movement.ProjectOntoPlane(hit.normal);
                skimDirection = skimDirection.normalized; 
                skimDirection*= movement.magnitude;
                if(Vector3.Angle(skimDirection,movement)<45)
                    movement = skimDirection;
            }
        }
        
        characterController.Move(movement*Time.deltaTime);


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = debugGroundedColor;
        Gizmos.DrawSphere(transform.position + 2.5f * Vector3.up, 0.5f);
    }
}
