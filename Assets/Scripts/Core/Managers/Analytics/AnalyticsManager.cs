﻿using System.Collections.Generic;
using System.Linq;
using Core.Managers.Events;
using System.IO;
using System.Text;
using UnityEngine;

namespace Core.Managers.Analytics
{
    public class AnalyticsManager
    {
        private static AnalyticsManager s_instance;

        private static AnalyticsManager Instance
        {
            get { return s_instance ??= new AnalyticsManager(); }
        }

        private readonly List<SessionAnalyticsData> _sessionData = new() { new SessionAnalyticsData() };
        private SessionAnalyticsData CurrentData => _sessionData.Last();

        public static AnalyticsManager Activate()
        {
            return Instance;
        }

        private AnalyticsManager()
        {
            EventManager.AddListener<AnalyticsEvent>(OnAnalyticsEventFired);
            EventManager.AddListener<StartSessionEvent>(OnSessionStarted);
            EventManager.AddListener<EndSessionEvent>(OnSessionEnded);
        }

        ~AnalyticsManager()
        {
            EventManager.RemoveListener<AnalyticsEvent>(OnAnalyticsEventFired);
            EventManager.RemoveListener<StartSessionEvent>(OnSessionStarted);
            EventManager.RemoveListener<EndSessionEvent>(OnSessionEnded);
        }

        private void OnAnalyticsEventFired(AnalyticsEvent @event)
        {
            CurrentData.CapturedEvents.Add(@event);
        }

        private void OnSessionStarted(StartSessionEvent @event)
        {
            CurrentData.Start();
        }

        private void OnSessionEnded(EndSessionEvent @event)
        {
            CurrentData.End();
            ExportSessionDataToCSV(CurrentData, $"{Application.persistentDataPath}\\Session_{_sessionData.Count}_AnalyticsData.csv");
            _sessionData.Add(new SessionAnalyticsData());
        }

        public void ExportSessionDataToCSV(SessionAnalyticsData sessionData, string filePath)
        {
            try
            {
                // Create a StreamWriter to write to the CSV file
                using var writer = new StreamWriter(filePath, false, Encoding.UTF8);

                // Write start and end date time. 
                writer.WriteLine($"StartDateTime,{sessionData.StartDateTime}");
                writer.WriteLine($"EndDateTime,{sessionData.EndDateTime}\n");
                writer.WriteLine("Captured Events");
                writer.WriteLine("Name, Parameter, Value");

                // Write the event and it's parameters
                foreach (var eventDataString in from @event in sessionData.CapturedEvents
                         let eventDataString = $"{@event.Name}"
                         select @event.Parameters.Select(parameter => $",{parameter.Name}, {parameter.Value}\n")
                             .Aggregate(eventDataString, (current, paramString) => current + paramString))
                {
                    writer.WriteLine(eventDataString);
                }

                writer.Close();
            }
            catch (IOException ex)
            {
                Debug.LogError("Error writing to CSV file: " + ex.Message);
            }
        }
    }
}