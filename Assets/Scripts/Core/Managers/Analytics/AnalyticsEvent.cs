using System.Collections.Generic;

namespace Core.Managers.Analytics
{
    public class AnalyticsEvent : GameEvent
    {
        public string EventName { get; private set; }

        public List<Parameter> Parameters { get; private set; }

        public AnalyticsEvent(string eventName)
        {
            EventName = eventName;
            Parameters = new List<Parameter>();
        }

        public void AddParameter(string parameterName, string parameterValue)
        {
            AddParameter(new Parameter(parameterName, parameterValue));
        }

        public void AddParameter(Parameter parameter)
        {
            Parameters.Add(parameter);
        }
    }
}