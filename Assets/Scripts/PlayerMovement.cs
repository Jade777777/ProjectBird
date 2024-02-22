using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float flySpeed = 5f;
    [SerializeField]
    private float rotSpeed = 1f;
    [SerializeField]
    private float mouseDeltaDeadZone = 0.1f;
    

    private CustomInput customInput = null;
    private Rigidbody rb;

    private Quaternion inputRotation = Quaternion.identity;

    private void Awake()
    {
        customInput = new CustomInput();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        customInput.Enable();
        customInput.Gameplay.Movement.performed += OnMovementPerformed;

    }

    private void OnDisable()
    {
        customInput.Disable();
        customInput.Gameplay.Movement.performed += OnMovementPerformed;

    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        if(!Mouse.current.leftButton.IsPressed()) { return; }
        Vector2 val = value.ReadValue<Vector2>();
        Debug.Log(val);
        if (Mathf.Abs(val.x) <= mouseDeltaDeadZone) val.x = 0;
        if(Mathf.Abs(val.y) <= mouseDeltaDeadZone) val.y = 0;

        val = val.normalized * rotSpeed;

        inputRotation = Quaternion.Euler(val.y, val.x, 0);
        transform.rotation *= inputRotation;

    }

    private void FixedUpdate()
    {
        /*transform.rotation *= inputRotation;
        inputRotation = Quaternion.identity;*/

        rb.velocity = transform.forward * flySpeed;

        //rb.AddForce(forceVector, ForceMode.Force);
    }
}
