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
    public GameObject streakMeter;

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
        streakMeter.SetActive(false);

        //Set onboarding state to the start and set the text variables
        onboardingState = 1;
        text = onboardingPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        buttonText = onboardingPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void Continue()
    {
        //Continue the onboarding process each time the continue button is clicked
        switch (onboardingState)
        {
            //Show controls and onboard the player
            case 1:
                onboardingPanel.GetComponent<RectTransform>().localPosition = new Vector3(-24, -341, 0);
                text.text = "The controls for the game are listed here in the bottom left. \n<--";
                controls.SetActive(true);
                onboardingState++;
                break;

            //Show flight meter and onboard the player
            case 2:
                onboardingPanel.GetComponent<RectTransform>().localPosition = new Vector3(0, 116, 0);
                text.text = "Up here is your flight meter. Press Spacebar when the circle is in the green zone to effectively flap your wings.";
                flightMeter.SetActive(true);
                onboardingState++;
                break;
            
            //Show streak meter and onboard the player
            case 3:
                onboardingPanel.GetComponent<RectTransform>().localPosition = new Vector3(59, 0, 0);
                text.text = "This meter represents your Flight Streak, it shows how accurate you are at maintaining the rhythm of flapping your wings. The higher your streak, the faster and higher you'll fly";
                text.fontSize = 35;
                streakMeter.SetActive(true);
                buttonText.text = "Start Game";
                onboardingState++;
                break;

            //Close the window and unpause the game
            case 4:
                onboardingPanel.SetActive(false);
                Time.timeScale = 1.0f;
                break;
        }
    } 
}
