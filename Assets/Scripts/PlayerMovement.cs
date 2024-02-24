using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraTarget;
    [SerializeField]
    private float maxFlySpeed = 5f;
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



    private float currentSpeed=0 ;
    private float currentYSpeed = 0;
    private float fallSpeed = 3f;
    private float maxRiseSpeed = 8f;


    private Vector2 prevMousePos = Vector2.zero;
    private Vector2 curMousePos = Vector2.zero;
    


    //Get bird prefab
    public GameObject birdPrefab;

    //Get bird animator
    Animator birdAnimator;

    private void Awake()
    {
        customInput = new CustomInput();
        characterController = GetComponent<CharacterController>();

        birdAnimator = birdPrefab.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        customInput.Enable();
        //customInput.Gameplay.CameraRevolve.performed += OnCameraRevolve;
    }

    private void OnDisable()
    {
        customInput.Disable();
       // customInput.Gameplay.CameraRevolve.performed -= OnCameraRevolve;

    }

    private void Start()
    {
        prevMousePos = Mouse.current.position.ReadValue();
    }
    private void Update()
    {
        HandleRhythm();
        HandleRotation();
        HandleCamera();
        Movement();

    }

    private void FixedUpdate()
    {

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

    private void HandleRhythm()
    {
        
        if (GetComponent<RhythmTracker>().IsFlapping)
        {
            currentYSpeed = maxRiseSpeed*GetComponent<RhythmTracker>().Accuracy;
            currentYSpeed = Mathf.Clamp(currentYSpeed, fallSpeed, maxRiseSpeed);
        }
        else
        {
            currentYSpeed = -fallSpeed;

        }
        if (GetComponent<CharacterController>().isGrounded)
        {
            currentSpeed = 0;
        }
        else
        {
            currentSpeed = GetComponent<RhythmTracker>().Accuracy * maxFlySpeed;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxFlySpeed);
     
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

        cameraTargetRotation =  Quaternion.AngleAxis(mouseDelta.x * camRotSpeed * Time.deltaTime, Vector3.up) * 
                                Quaternion.AngleAxis(mouseDelta.y * camRotSpeed * Time.deltaTime, Vector3.right);

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
        characterController.Move((transform.forward * currentSpeed + transform.up * currentYSpeed) * Time.deltaTime);

    }
}
