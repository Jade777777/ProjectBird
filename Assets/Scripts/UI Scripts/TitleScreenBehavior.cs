using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Core.Managers.Analytics;
using Core.Managers.Events;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Screenshot;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class TitleScreenBehavior : MonoBehaviour
{
    public ScreenshotUtil screenshotScript;
    public GameObject screenshotListContent;
    public GameObject scrollbar;
    public GameObject optionsMenu;
    public GameObject titleScreenUI;
    public GameObject screenshotUI;

    bool screenshotsLoaded = false;

    private void Start()
    {
        // Activate Event Manager Before Analytics manager.
        EventManager.Activate();
        AnalyticsManager.Activate();
    }

    //Transitions to the Game Scene
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    //Quits the Game
    public void QuitGame()
    {
        Application.Quit();
    }

    //Changes whether the options screen is shown or not
    public void ToggleOptions()
    {
        optionsMenu.SetActive(!optionsMenu.activeInHierarchy);
        titleScreenUI.SetActive(!titleScreenUI.activeInHierarchy);
    }

    public void GoToScreenshots()
    {
        titleScreenUI.SetActive(false);
        screenshotUI.SetActive(true);

        if (!screenshotsLoaded)
        {
            LoadScreenshots();
        }
    }

    public void ExitScreenshots()
    {
        titleScreenUI.SetActive(true);
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
