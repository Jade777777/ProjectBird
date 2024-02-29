using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class RhythmTracker : MonoBehaviour
{
    [Header("Target Range")]

    [SerializeField, Range(0, 1)]
    float targetMax=0.95f;
    [SerializeField, Range(0, 1)]
    float targetSize = 0.7f;

    [Header("Speed in Flaps/Second")]
    [SerializeField, Tooltip("The rate of flaping when at a minumum streak")]
    float maxVelocity = 5f;
    [SerializeField, Tooltip("The rate of flaping when at a maximum streak")]
    float minVelocity = 1f;


    [Header("Streak")]
    [SerializeField, Tooltip("The streak that must be achieved to reach maximum accuracy")]
    int maxStreak = 10;
    [SerializeField, Tooltip("The rate at which the streak decays when not flapping at a rate of Streaks/Second")]
    float streakDecay = 0.2f;
    [SerializeField, Tooltip("Increased rate of streak decay when condition is met")]
    float decayMultiplier = 4f;

    [Header("Debug Info")]
    [SerializeField, Tooltip("The current streak, this should be zero when the game is not running")]
    float currentStreak = 0;

    /// <summary>
    /// When this value is true, the streak will not decay.
    /// </summary>
    public bool ProtectStreak = false;
    /// <summary>
    /// Range of 0 to 1, 0 being downward wing position, 1 being upward.
    /// </summary>
    public float CurrentPosition { get; private set; }
    /// <summary>
    /// Range of 0 to 1, weight of current streak compared to max streak.
    /// </summary>
    public float Accuracy { get { return (float)currentStreak / (float)maxStreak; } }
    /// <summary>
    /// How long it takes to do one full flap of the wings.
    /// </summary>
    public float CurrentVelocity { get { return Mathf.Lerp(maxVelocity, minVelocity, (float)currentStreak / (float)maxStreak)/2; } }
    /// <summary>
    /// Is the current position within range to increase the streak.
    /// </summary>
    public bool IsTarget { get { return CurrentPosition > targetMax-targetSize && CurrentPosition < targetMax; } }
    public bool IsOverTarget { get { return CurrentPosition > targetMax; } }
    /// <summary>
    /// Is the player currently flapping the wings downward.
    /// </summary>
    public bool IsFlapping { get; private set; }


    public bool isCharging { get; private set; }
    public Action Flap;
    float lastFlap = 0;
    float cooldown = 1;
    float ChargeSpeed = 1;

    /// <summary>
    /// Was the last flap successful.
    /// </summary>
    public bool IsSuccess { get; private set; }

    private float currentDecayMultiplier;

    private CustomInput customInput = null;

    Animator birdAnimator;

    private void Awake()
    {
        Flap += () => Debug.Log("Flap");
        customInput = new CustomInput();
        birdAnimator = gameObject.GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        customInput.Enable();
        customInput.Gameplay.Flap.performed+=OnFlap;
    }
    private void OnDisable()
    {
        customInput.Disable();
        customInput.Gameplay.Flap.performed -= OnFlap;

    }

    private void Update()
    {
        if (Time.time< lastFlap + cooldown)
        {
            Debug.Log("Cooldown");
            return;
        }
        else
        {
            IsFlapping = false;
        }
        if (isCharging)
        {
           
            CurrentPosition += Time.deltaTime * ChargeSpeed;

            Debug.Log("Charging");
            if (CurrentPosition < targetMax && CurrentPosition > targetMax - targetSize)
            {
                Debug.Log("InRange");


            }

        }

        //Clamping manualy because IsFlapping needs to be set
        if (CurrentPosition < 0)
        {
            
            CurrentPosition = 0;
        }

        if(CurrentPosition > 1)
        {
            CurrentPosition = 1;
        }
        
    }

    /// <summary>
    /// Called by Unitys input system.
    /// </summary>
    /// <param name="value"></param>
    public void OnFlap(InputAction.CallbackContext value)
    {
        if (customInput.Gameplay.Flap.IsPressed())
        {
            isCharging = true;
        }
        else
        {

            isCharging = false;
            if (CurrentPosition < targetMax && CurrentPosition > targetMax - targetSize)
            {
                lastFlap = Time.time;
                IsFlapping = true;
                birdAnimator.SetTrigger("Flap");
                Flap.Invoke();
                
            }

                CurrentPosition = 0;

        }
    }


    public void ResetStreak()
    {
        //currentStreak = 0;
    }
    public void IncreasedDecay(bool val)
    {
        if (val)
        {
            //currentDecayMultiplier = decayMultiplier;
        }
        else
        {
            //currentDecayMultiplier = 1;
        }
    }



    //Debug code to help visualize success
    private void OnDrawGizmos()
    {
        if (!IsFlapping&& IsTarget)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawSphere(transform.position+Vector3.up*1.2f, 0.3f);
        if (IsSuccess)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawSphere(transform.position + Vector3.up * 1.7f, 0.15f);
    }
}