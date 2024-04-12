using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class StaticRotator : MonoBehaviour
    {
        [SerializeField] private float angularSpeed;
        
        private void Update()
        {
            transform.Rotate(Vector3.up, angularSpeed * Time.deltaTime);
        }
    }
}