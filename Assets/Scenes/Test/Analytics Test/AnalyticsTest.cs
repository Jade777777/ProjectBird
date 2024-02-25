using Core.Managers;
using Core.Managers.Analytics;
using UnityEngine;

public class AnalyticsTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AnalyticsManager.Activate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick(int buttonNumber)
    {
        new AnalyticsEvent("Button Clicked")
            .AddParameter("Number", buttonNumber.ToString())
            .AddParameter("Random add check", 69.ToString())
            .Raise();
    }

    public void OnStartSessionClicked()
    {
        new StartSessionEvent().Raise();
    }

    public void OnEndSessionClicked()
    {
        new EndSessionEvent().Raise();
    }
}
