using System;
using System.Collections.Generic;

namespace Core.Managers.Analytics
{
    public class SessionAnalyticsData
    {
        public DateTime StartDateTime { get; private set; }
        public DateTime EndDateTime { get; private set; }
        public List<AnalyticsEvent> CapturedEvents;

        public bool HasStarted { get; private set; }

        public void Start()
        {
            StartDateTime = DateTime.Now;
            HasStarted = true;
            CapturedEvents = new List<AnalyticsEvent>();
        }

        public void End()
        {
            EndDateTime = DateTime.Now;
        }
    }
}