using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Screenshot;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

//UI Script originally developed by Kevin Insinna
public class PauseScreenBehavior : MonoBehaviour
{
    //Variables
    bool isPaused = false;
    public GameObject pauseScreen;
    public GameObject gameUI;
    public GameObject onboardingPanel;

    public ScreenshotUtil screenshotScript;
    public GameObject screenshotListContent;
    public GameObject scrollbar;
    public GameObject screenshotUI;

    bool screenshotsLoaded = false;

    private void Update()
    {
        //Pause when Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape) && !onboardingPanel.activeInHierarchy && !screenshotUI.activeInHierarchy)
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

    public void GoToScreenshots()
    {
        pauseScreen.SetActive(false);
        screenshotUI.SetActive(true);

        if (!screenshotsLoaded)
        {
            LoadScreenshots();
        }
    }

    public void ExitScreenshots()
    {
        pauseScreen.SetActive(true);
        screenshotUI.SetActive(false);

        foreach(Transform child in screenshotListContent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void LoadScreenshots()
    {
        if (screenshotScript.ScreenshotData.Count != 0)
        {
            foreach (Tuple<System.DateTime, UnityEngine.Sprite> screenshot in screenshotScript.ScreenshotData)
            {
                GameObject screenshotObj = new GameObject();
                Image NewImage = screenshotObj.AddComponent<Image>(); 
                screenshotObj.GetComponent<RectTransform>().sizeDelta = new Vector2 (1280, 720);
                NewImage.sprite = screenshot.Item2; 
                screenshotObj.GetComponent<RectTransform>().SetParent(screenshotListContent.transform); 
                screenshotObj.SetActive(true); 
            }
        }
    }
}

