using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class RhythmTracker : MonoBehaviour
{
    [SerializeField]
    float flapVelocity =1.0f;
    [SerializeField]
    float steadyVelocity = 1.0f;
    [SerializeField]
    float currentSteadyTarget = 0.9f;
    [SerializeField]
    float steadyDecayVelocity = 0.1f;

    [SerializeField]
    float tollerance = 0.01f;


    [SerializeField]
    float currentPosition = 0;
    [SerializeField]
    bool isFlapping = false;

    bool streak = false;


    private void Update()
    {
       
        if (isFlapping && currentPosition > 0)
        {
            currentPosition-=flapVelocity*Time.deltaTime;
        }
        else
        {
            isFlapping = false;
            currentPosition+=steadyVelocity*Time.deltaTime;

            if(currentPosition >= currentSteadyTarget)
            {
                currentSteadyTarget -= steadyDecayVelocity*Time.deltaTime;
            }
            
        }


        currentPosition = Mathf.Clamp(currentPosition,0,currentSteadyTarget);
        currentSteadyTarget = Mathf.Clamp(currentSteadyTarget, tollerance, 1);
        GetComponent<ProcAnimFlap>().WingPos = currentPosition;
    }

    //TODO: player should only flap down, not up
    public void OnFlap(InputValue value)
    {
        if (!isFlapping && value.isPressed && currentPosition > currentSteadyTarget - tollerance && currentPosition < currentSteadyTarget)
        {
            currentSteadyTarget += 0.1f;
            Debug.Log("Down Correct");
            streak = true;
        }


        if (value.isPressed)
        {
            isFlapping = value.isPressed;
        }
    }
    private void OnDrawGizmos()
    {
        if (!isFlapping&&currentPosition > currentSteadyTarget - tollerance && currentPosition < currentSteadyTarget)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawSphere(transform.position+Vector3.up*1.2f, 0.3f);
    }
}