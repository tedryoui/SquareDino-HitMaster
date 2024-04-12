using System;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class Enemy : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SliderPanel healthBarPanel;
        [SerializeField] private Ragdoll ragdoll;
        [SerializeField] private Animator animator;
        
        [Header("Data")]
        [SerializeField] private int maxHealthPoints;
        
        private int _currentHealthPoints;

        public bool IsDead => _currentHealthPoints <= 0;

        private void Awake()
        {
            _currentHealthPoints = maxHealthPoints;
            
            healthBarPanel.SetEnabled(false);
        }

        public void Damage(int damage = 1)
        {
            if (!IsDead)
            {
                _currentHealthPoints -= damage;

                healthBarPanel.SetEnabled(true);
                healthBarPanel.SetFillAmount((float)_currentHealthPoints / (float)maxHealthPoints);
                
                if (IsDead)
                {
                    healthBarPanel.SetEnabled(false);
                    
                    ragdoll.enabled = true;
                    animator.enabled = false;
                }
            }
        }

        public void Punch(Vector3 point, Vector3 direction, float force)
        {
            ragdoll.AddForce(point, direction, force);
        }
    }
}