using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    bool powerFlap = false;
    private void Update()
    {
        if (isFlapping && currentPosition > 0)
        {
            currentPosition-=flapVelocity*Time.deltaTime;
        }
        else
        {

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
    public void OnFlap(InputValue value)
    {
        if (value.isPressed && currentPosition > currentSteadyTarget-tollerance && currentPosition < currentSteadyTarget)
        {
            Debug.Log("Down Correct");
            powerFlap = true;
        }
        if (!value.isPressed && powerFlap && currentPosition <= tollerance)
        {
            Debug.Log("Up Correct, Power Flap!");
            currentSteadyTarget += 0.1f;
            powerFlap =false;
        }

        isFlapping =  value.isPressed;
    }
}