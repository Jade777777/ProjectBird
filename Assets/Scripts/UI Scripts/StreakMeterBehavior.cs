using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StreakMeterBehavior : MonoBehaviour
{
    //References to UI slider and RhythmTracker script
    Slider streakSlider;
    public RhythmTracker rhythmTracker;

    // Start is called before the first frame update
    void Start()
    {   
        streakSlider = this.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {   
        //Set the value of the slider to the accuracy of the rhythm tracker
        streakSlider.value = rhythmTracker.Accuracy;
    }
}
