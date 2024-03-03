using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//UI Script originally developed by Kevin Insinna
public class PauseScreenBehavior : MonoBehaviour
{
    //Variables
    bool isPaused = false;
    public GameObject pauseScreen;
    public GameObject gameUI;
    public GameObject onboardingPanel;

    private void Update()
    {
        //Pause when Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape) && !onboardingPanel.activeInHierarchy)
        {
            PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.H) && !onboardingPanel.activeInHierarchy && !isPaused)
        {
            ToggleHUD();
        }
    }

    //Toggles the game being paused
    public void PauseGame()
    {
        isPaused = !isPaused;
        Cursor.visible = isPaused;
        if (isPaused)
        {
            //Toggle pause UI on and game UI off
            pauseScreen.gameObject.SetActive(true);
            gameUI.gameObject.SetActive(false);
            Time.timeScale = 0.0f;

            Cursor.lockState = CursorLockMode.None;
        }

        else
        {
            //Toggle pause UI off and game UI on
            pauseScreen.gameObject.SetActive(false);
            gameUI.gameObject.SetActive(true);
            Time.timeScale = 1.0f;

            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    //Goes back to Title Screen
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void ToggleHUD()
    {
        gameUI.SetActive(!gameUI.activeInHierarchy);
    }
}

