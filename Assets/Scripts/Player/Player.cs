using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private SliderPanel sliderPanel;
        [SerializeField] private WaypointQueue waypointQueue;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Animator animator;

        [Header("Data")]
        [SerializeField] private float rotationDampSpeed = 0.35f;

        [Header("Shooting")]
        [SerializeField] private Transform shootPosition;
        [SerializeField] private Weapon.WeaponData weaponData;

        private ObjectPool<Bullet> _bulletPool;
        
        private const float MinimalVectorComparision = 0.25f;
        private const string AnimatorVelocityParameter = "velocity";

        #region Unity Messages

        private void Awake()
        {
            GameMode.ModeChanged.AddListener(OnGameModeChanged);
            OnGameModeChanged(GameMode.Current);
            
            PlayerMode.SetMode(PlayerMode.Mode.Walk);
        }

        private void Start()
        {
            WarpToBasePosition();
            ObjectPoolBullets();
        }

        private void Update()
        {
            UpdateDestination();
            
            if (PlayerMode.Current is PlayerMode.Mode.Fight)
                AlignRotation(waypointQueue.Current.transform.rotation);
        }

        private void OnDestroy()
        {
            GameMode.ModeChanged.RemoveListener(OnGameModeChanged);
        }
        
        #endregion

        #region Waypoint Movement

        private void WarpToBasePosition()
        {
            var first = waypointQueue.Waypoints.First();
            
            navMeshAgent.Warp(first.transform.position);
            
            ObserveCurrentWaypoint(first);
            
            sliderPanel.SetFillAmount(1.0f - (float)(waypointQueue.QueueLength + 1) / (float)waypointQueue.Waypoints.Length);
        }

        private void UpdateDestination()
        {
            animator.SetFloat(AnimatorVelocityParameter, navMeshAgent.velocity.magnitude);
            
            if (Vector3.Distance(navMeshAgent.destination, waypointQueue.Current.transform.position) >= MinimalVectorComparision)
            {
                navMeshAgent.SetDestination(waypointQueue.Current.transform.position);
            }
            else if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                ObserveCurrentWaypoint(waypointQueue.Current);
            }
        }

        private void ObserveCurrentWaypoint(Waypoint waypointQueueCurrent)
        {
            if (waypointQueueCurrent.IsCleared)
            {
                if (waypointQueue.QueueLength == 0)
                {
                    PlayerMode.SetMode(PlayerMode.Mode.Idle);
                    
                    sliderPanel.SetFillAmount(1.0f);
                }
                else
                {
                    waypointQueue.Next();
                    PlayerMode.SetMode(PlayerMode.Mode.Walk);
                }
            }
            else if (PlayerMode.Current is not PlayerMode.Mode.Fight)
            {
                PlayerMode.SetMode(PlayerMode.Mode.Fight);
                
                sliderPanel.SetFillAmount(1.0f - (float)(waypointQueue.QueueLength) / (float)waypointQueue.Waypoints.Length);
            }
        }

        private void AlignRotation(Quaternion rotation)
        {
            var quaternion = Quaternion.Slerp(transform.rotation, rotation, rotationDampSpeed);

            transform.rotation = quaternion;
        }

        #endregion
        
        #region Shooting

        private void ObjectPoolBullets()
        {
            _bulletPool = new ObjectPool<Bullet>(CreateFuncBullet, ActionOnGetBullet, ActionOnReleaseBullet, ActionOnDestroy);
        }

        private void ActionOnDestroy(Bullet bullet)
        {
            Object.Destroy(bullet.gameObject);
        }

        private void ActionOnReleaseBullet(Bullet bullet)
        {
            bullet.dispose.RemoveListener(ReleaseBullet);
            
            bullet.gameObject.SetActive(false);
        }

        private void ActionOnGetBullet(Bullet bullet)
        {
            bullet.dispose.AddListener(ReleaseBullet);
            
            bullet.gameObject.SetActive(true);
        }

        private Bullet CreateFuncBullet()
        {
            return weaponData.Construct();
        }

        private void ReleaseBullet(Bullet bullet)
        {
            _bulletPool.Release(bullet);
        }

        public void Shoot(Vector3 destination)
        {
            if (PlayerMode.Current is not PlayerMode.Mode.Fight) return;
            
            var bullet = _bulletPool.Get();
            var direction = destination - shootPosition.position;

            bullet.SetPosition(shootPosition.position);
            bullet.SetDirection(direction);
        }
        
        #endregion

        private void OnGameModeChanged(GameMode.Mode mode)
        {
            enabled = mode == GameMode.Mode.Play;

            navMeshAgent.enabled = enabled;

        }
    }
}