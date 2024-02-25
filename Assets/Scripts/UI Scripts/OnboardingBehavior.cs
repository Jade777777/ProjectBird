using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnboardingBehavior : MonoBehaviour
{
    //References to UI panels
    public GameObject onboardingPanel;
    public GameObject controls;
    public GameObject flightMeter;

    //Variables
    int onboardingState;
    TextMeshProUGUI text;
    TextMeshProUGUI buttonText;

    // Start is called before the first frame update
    void Start()
    {
        //Pause Game and disable game UI elements
        Time.timeScale = 0.0f;
        controls.SetActive(false);
        flightMeter.SetActive(false);

        //Set onboarding state to the start and set the text variables
        onboardingState = 1;
        text = onboardingPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        buttonText = onboardingPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void Continue()
    {
        switch (onboardingState)
        {
            case 1:
                onboardingPanel.GetComponent<RectTransform>().localPosition = new Vector3(-24, -341, 0);
                text.text = "The controls for the game are listed here in the bottom left. \n<--";
                controls.SetActive(true);
                onboardingState++;
                break;

            case 2:
                onboardingPanel.GetComponent<RectTransform>().localPosition = new Vector3(0, 116, 0);
                text.text = "Up here is your flight meter. Press Spacebar when the circle is in the green zone to effectively flap your wings.";
                buttonText.text = "Start Game";
                flightMeter.SetActive(true);
                onboardingState++;
                break;

            case 3:
                onboardingPanel.SetActive(false);
                Time.timeScale = 1.0f;
                break;
        }
    } 
}
