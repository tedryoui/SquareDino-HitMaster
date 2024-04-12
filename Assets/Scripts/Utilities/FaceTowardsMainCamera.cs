using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class FaceTowardsMainCamera : MonoBehaviour
    {
        [SerializeField] private bool flipX;

        private void Start()
        {
            if (flipX) transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        private void Update()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}