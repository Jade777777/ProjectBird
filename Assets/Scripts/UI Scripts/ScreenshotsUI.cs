using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Screenshot;
using UnityEngine.Networking;

public class ScreenshotsUI : MonoBehaviour
{
    public ScreenshotUtil screenshotScript;
    public GameObject screenshotListContent;
    public GameObject scrollbar;
    public GameObject titleUI;
    public GameObject screenshotUI;

    bool screenshotsLoaded = false;

    public void GoToScreenshots()
    {
        titleUI.SetActive(!titleUI.activeInHierarchy);
        screenshotUI.SetActive(!screenshotUI.activeInHierarchy);

        if (!screenshotsLoaded)
        {
            LoadScreenshots();
        }
    }

    void LoadScreenshots()
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

        screenshotsLoaded = true;
    }
}
