using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraTarget;
    [SerializeField]
    private float flySpeed = 5f;
    [SerializeField]
    private float rotSpeed = 1f;    
    [SerializeField]
    private float camRotSpeed = 1f;
    [SerializeField]
    private float maxCameraXRot = 45f;
    [SerializeField]
    private float mouseDeltaDeadZone = 0.1f;
    

    private CustomInput customInput = null;
    private Rigidbody rb;

    private Quaternion inputRotation = Quaternion.identity;
    private Quaternion cameraTargetRotation = Quaternion.identity;


    //Get bird prefab
    public GameObject birdPrefab;

    //Get bird animator
    Animator birdAnimator;

    private void Awake()
    {
        customInput = new CustomInput();
        rb = GetComponent<Rigidbody>();

        birdAnimator = birdPrefab.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        customInput.Enable();
        customInput.Gameplay.CameraRevolve.performed += OnCameraRevolve;
    }

    private void OnDisable()
    {
        customInput.Disable();
        customInput.Gameplay.CameraRevolve.performed -= OnCameraRevolve;

    }

    private void Update()
    {
        HandleRotation();
        
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
            birdAnimator.SetTrigger("Fly");
            birdAnimator.SetBool("flying", true);
            Debug.Log("forward");
            return;
        }

        inputRotation = Quaternion.Euler(0, val * rotSpeed, 0);
    }

    // Camera revolves with mouse move if LMB is held down
    private void OnCameraRevolve(InputAction.CallbackContext value)
    {
        if (!Mouse.current.leftButton.isPressed) return;

        Vector2 val = value.ReadValue<Vector2>();

        if (Mathf.Abs(val.x) <= mouseDeltaDeadZone) val.x = 0;
        if (Mathf.Abs(val.y) <= mouseDeltaDeadZone) val.y = 0;

        val.Normalize();

        cameraTargetRotation = Quaternion.Euler(val.y * camRotSpeed, val.x * camRotSpeed, 0);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * flySpeed * Time.deltaTime;

        // Player Rotation
        transform.rotation *= inputRotation;
        inputRotation = Quaternion.identity;

        // Camera Rotation
        cameraTarget.transform.rotation *= cameraTargetRotation;
        cameraTargetRotation = Quaternion.identity;


        // Clamping rotation on x axis to prevent camera from going upside-down
        var angles = cameraTarget.transform.localEulerAngles;
        if(angles.x > 180)
        {
            angles.x = Mathf.Clamp(angles.x, 360 - maxCameraXRot, 360);
        }
        else
        {
            angles.x = Mathf.Clamp(angles.x, 0, maxCameraXRot);
        }

        cameraTarget.transform.localEulerAngles = angles;

        
    }



}
