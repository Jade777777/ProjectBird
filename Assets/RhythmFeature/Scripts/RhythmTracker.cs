using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RhythmTracker : MonoBehaviour
{
    public void OnFlap(InputValue value)
    {
        Debug.Log("Flap"+ value.isPressed);
    }
}
