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

    public List<GameObject> nestColliders;

    //Variables
    PlayerMovement player;
    int onboardingState;
    TextMeshProUGUI text;
    TextMeshProUGUI buttonText;
    TextMeshProUGUI controlsText;

    private Vector3 startingPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //Pause Game and disable game UI elements
        //Time.timeScale = 0.0f;

        controls.SetActive(false);
        flightMeter.SetActive(false);
        streakMeter.SetActive(false);

        //Set onboarding state to the start and set the text variables
        onboardingState = 1;
        text = onboardingPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        buttonText = onboardingPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        controlsText = controls.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        startingPos = onboardingPanel.GetComponent<RectTransform>().localPosition;
        player = GameObject.Find("Player Y Integration").GetComponent<PlayerMovement>();
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
                controlsText.text = "Use WASD to move around on the ground.\n\nPress E to interact.\n\nLeftClick + move your mouse to control the camera. ";
                onboardingState++;
                break;

            case 2:
                onboardingPanel.GetComponent<RectTransform>().localPosition = startingPos;
                text.fontSize = 38;
                text.text = "Your chicks are getting hungry! Luckily there is an almond tree right next to the nest.\n Walk over to it and try to pick it.";
                onboardingState++;
                break;
            case 3:
                player.canMove = true;
                onboardingPanel.SetActive(false);
                break;
            case 4:
                player.canMove = false;
                onboardingPanel.SetActive(true);
                text.text = "You just collected an almond! You can carry a maximum of 3 almonds at a time.\n Try feeding to one of your chicks!";
                onboardingState++;
                break;
            case 5:
                player.canMove = true;
                onboardingPanel.SetActive(false);
                break;
            case 6:
                player.canMove = false;
                onboardingPanel.SetActive(true);
                text.fontSize = 44;
                text.text = "You just fed one of your chicks!\n It is now strong enough to fly around with you!";
                onboardingState++;
                break;
            case 7:
                text.fontSize = 38;
                text.text = "You still have hungry chicks in the nest.\n Try finding other almond trees in the environment and bring them back to your nest.";
                onboardingState++;
                break;
            //Show flight meter and onboard the player
            case 8:
                onboardingPanel.GetComponent<RectTransform>().localPosition = new Vector3(-24, -341, 0);
                text.text = "The controls for flying are listed here. \n<--";
                controlsText.text = "Hold Spacebar: Flap Wings\r\n-A and D: Turn\r\n-W or S: Dive\r\n-Left Click + Move Mouse: Look Around";
                onboardingState++;
                break;
            case 9:
                onboardingPanel.GetComponent<RectTransform>().localPosition = new Vector3(0, 116, 0);
                text.text = "Up here is your flight meter. Hold spacebar to charge and release when the circle is in the green zone to effectively flap your wings.";
                flightMeter.SetActive(true);
                onboardingState++;
                break;
            
            //Show streak meter and onboard the player
            case 10:
                onboardingPanel.GetComponent<RectTransform>().localPosition = new Vector3(59, 0, 0);
                text.text = "This meter represents your Flight Streak, it shows how accurate you are at maintaining the rhythm of flapping your wings. The higher your streak, the faster and higher you'll fly";
                text.fontSize = 35;
                streakMeter.SetActive(true);
                buttonText.text = "Start Game";
                onboardingState++;
                break;

            //Close the window and unpause the game
            case 11:
                player.canMove = true;
                player.canFly = true;
                foreach(GameObject col in nestColliders)
                {
                    col.SetActive(false);
                }
                onboardingPanel.SetActive(false);
                Cursor.visible = false;
                break;
        }
    } 

    public void OnCollection()
    {
        if(onboardingState == 3)
        {
            onboardingState++;
            Continue();
        }
    }

    public void OnFeed()
    {
        if(onboardingState == 5)
        {
            onboardingState++;
            Continue();
        }
    }
}
