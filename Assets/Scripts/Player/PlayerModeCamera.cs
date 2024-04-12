using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class PlayerModeCamera : MonoBehaviour
    {
        private CinemachineVirtualCamera _virtualCamera;

        [SerializeField] private PlayerMode.Mode requiredMode;
        
        private IEnumerator Start()
        {
            yield return null;
            
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            
            PlayerMode.ModeChanged.AddListener(OnPlayerModeChanged);
            OnPlayerModeChanged(PlayerMode.Current);
        }

        private void OnDestroy()
        {
            PlayerMode.ModeChanged.RemoveListener(OnPlayerModeChanged);
        }

        private void OnPlayerModeChanged(PlayerMode.Mode mode)
        {
            enabled = mode == requiredMode;

            _virtualCamera.enabled = enabled;
        }
    }
}