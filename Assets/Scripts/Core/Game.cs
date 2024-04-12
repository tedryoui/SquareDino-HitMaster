using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class Game : MonoBehaviour
    {
        private Coroutine _activeRoutine;

        private void Awake()
        {
            _activeRoutine = null;
            
            PlayerMode.ModeChanged.AddListener(OnPlayerModeChanged);
            OnPlayerModeChanged(PlayerMode.Current);
        }

        private void OnDestroy()
        {
            PlayerMode.ModeChanged.RemoveListener(OnPlayerModeChanged);
        }

        private void OnPlayerModeChanged(PlayerMode.Mode mode)
        {
            if (_activeRoutine == null)
            {
                if (PlayerMode.Current is PlayerMode.Mode.Idle) _activeRoutine = StartCoroutine(WinRoutine());
            }
        }

        private IEnumerator WinRoutine()
        {
            yield return new WaitForSeconds(2.0f);

            GameMode.SetMode(GameMode.Mode.Paused);
            
            var asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

            asyncOperation.completed += _ => _activeRoutine = null;
            asyncOperation.allowSceneActivation = true;
        }
    }
}