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
    float targetMaxSize = 0.25f;
    [SerializeField, Range(0, 1)]
    float targetMinSize = 0.25f;

    [Header("Speed")]
    [SerializeField, Tooltip("The rate it takes to ready a flap")]
    float chargeSpeed = 5f;

    [Header("Streak")]
    [SerializeField, Tooltip("The streak that must be achieved to reach maximum accuracy")]
    int maxStreak = 10;
    [SerializeField, Tooltip("The rate at which the streak decays when not flapping at a rate of Streaks/Second")]
    float streakDecay = 0.2f;

    [Header("Debug Info")]
    [SerializeField, Tooltip("The current streak, this should be zero when the game is not running")]
    float currentStreak = 0;


    /// <summary>
    /// Range of 0 to 1, 0 being downward wing position, 1 being upward.
    /// </summary>
    public float CurrentPosition { get; private set; }
    /// <summary>
    /// Range of 0 to 1, weight of current streak compared to max streak.
    /// </summary>
    public float Accuracy { get { return (float)currentStreak / (float)maxStreak; } }

    /// <summary>
    /// Is the current position within range to increase the streak.
    /// </summary>
    public bool IsTarget { get { return CurrentPosition < targetMax && CurrentPosition > targetMax - targetMaxSize; } }

    /// <summary>
    /// Is the player currently flapping the wings downward.
    /// </summary>
    public bool IsFlapping { get; private set; }


    public bool isCharging { get; private set; }
    public Action Flap;
    float lastFlap = 0;
    float cooldown = 1;


    /// <summary>
    /// Was the last flap successful.
    /// </summary>
    public bool IsSuccess { get; private set; }



    private CustomInput customInput = null;

    Animator birdAnimator;

    private void Awake()
    {
        Flap += () => { };
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
            CurrentPosition += Time.deltaTime * chargeSpeed;
            Debug.Log("Charging");
            if (IsTarget)
            {
                Debug.Log("InRange");
            }

        }
        CurrentPosition = Mathf.Clamp01(CurrentPosition);
        currentStreak -= streakDecay * Time.deltaTime;
        currentStreak = Mathf.Clamp(currentStreak, 0, maxStreak);
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
            if (IsTarget)
            {
                currentStreak++;
                currentStreak = Mathf.Clamp(currentStreak, 0, maxStreak);
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
        currentStreak = 0;
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