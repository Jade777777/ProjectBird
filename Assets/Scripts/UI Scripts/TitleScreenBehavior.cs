using System;
using System.Collections;
using System.Collections.Generic;
using Core.Managers;
using Core.Managers.Analytics;
using Core.Managers.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenBehavior : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject titleScreenUI;

    private void Start()
    {
        // Event manager should activate first.
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
}
