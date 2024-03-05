using System;
using System.Globalization;
using System.Linq;
using Core.Managers;
using Core.Managers.Analytics;
using UnityEngine;

namespace Utilities
{
    public class ChickChecker : MonoBehaviour
    {
        private GameObject[] _chicks;

        private void Start()
        {
            _chicks = GameObject.FindGameObjectsWithTag("SmallBird");
        }

        private void Update()
        {
            if (!_chicks.Any())
                new AllChicksFedEvent("Chicks Fed").Raise();
        }
    }

    public class AllChicksFedEvent : AnalyticsEvent
    {
        public readonly DateTime time = DateTime.Now;

        public AllChicksFedEvent(string name) : base(name)
        {
            AddParameter("Time", time.ToString(CultureInfo.InvariantCulture));
        }
    }
}