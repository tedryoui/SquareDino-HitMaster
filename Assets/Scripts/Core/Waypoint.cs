using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] private Enemy[] enemies;

        public bool IsCleared => enemies.All(x => x.IsDead);
    }
}