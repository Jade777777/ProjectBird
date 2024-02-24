using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlightTImingUI : MonoBehaviour
{
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
        flightSlider.value = rhythmTracker.CurrentPosition;
    }
}
