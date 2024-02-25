using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlightTImingUI : MonoBehaviour
{   
    //References to UI slider and RhythmTracker script
    Slider flightSlider;
    public RhythmTracker rhythmTracker;

    // Start is called before the first frame update
    void Start()
    {   
        flightSlider = this.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {   
        //Set the value of the slider to the currentposition of the rhythm tracker
        flightSlider.value = rhythmTracker.CurrentPosition;
    }
}
