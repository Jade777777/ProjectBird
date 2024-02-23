using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class RhythmTracker : MonoBehaviour
{
    [Header("Target Range")]
    [SerializeField, Range(0, 1)]
    float targetMin=0.7f;
    [SerializeField, Range(0, 1)]
    float targetMax=0.95f;


    [Header("Speed in Flaps/Second")]
    [SerializeField, Tooltip("The rate of flaping when at a minumum streak")]
    float maxVelocity = 5f;
    [SerializeField, Tooltip("The rate of flaping when at a maximum streak")]
    float minVelocity = 1f;


    [Header("Streak")]
    [SerializeField, Tooltip("The streak that must be achieved to reach maximum accuracy")]
    int maxStreak = 10;

    [SerializeField, Tooltip("The current streak, this should be zero when the game is not running")]
    int currentStreak = 0;


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
    public bool IsTarget { get { return CurrentPosition > targetMin && CurrentPosition < targetMax; } }
    /// <summary>
    /// Is the player currently flapping the wings downward.
    /// </summary>
    public bool IsFlapping { get; private set; }
    /// <summary>
    /// Was the last flap successful.
    /// </summary>
    public bool IsSuccess { get; private set; }

    private CustomInput customInput = null;
    private void Awake()
    {
        
        customInput = new CustomInput();
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
        if(IsFlapping)
        {
            CurrentPosition -= CurrentVelocity * Time.deltaTime;
        }
        else
        {
            CurrentPosition += CurrentVelocity * Time.deltaTime;
        }
        
        //Clamping manualy because IsFlapping needs to be set
        if (CurrentPosition < 0)
        {
            CurrentPosition = 0;
            IsFlapping = false;
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
        if (!IsFlapping)// only valid input is on upswing
        {
            if (IsTarget)
            {
                currentStreak++;
                IsSuccess = true;
            }
            else
            {
                currentStreak--;
                IsSuccess = false;
            }
            currentStreak = Mathf.Clamp(currentStreak, 0, maxStreak);
            IsFlapping = true;
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