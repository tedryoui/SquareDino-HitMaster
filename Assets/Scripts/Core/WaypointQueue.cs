using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class WaypointQueue : MonoBehaviour
    {
        [SerializeField] private Waypoint[] waypoints;

        private Waypoint _current;
        private Queue<Waypoint> _queue;

        public Waypoint Current => _current;
        public Waypoint[] Waypoints => waypoints;

        public int QueueLength => _queue.Count;

        private void Awake()
        {
            _queue = new Queue<Waypoint>(waypoints);
            _current = _queue.Dequeue();
        }

        public void Next()
        {
            if (_queue.Count == 0)
                return;
            
            _current = _queue.Dequeue();
        }
    }
}