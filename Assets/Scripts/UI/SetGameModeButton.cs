using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Button))]
    public class SetGameModeButton : MonoBehaviour
    {
        private Button _button;

        [SerializeField] private GameMode.Mode modeToSet;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(ChangeGameMode);
        }

        private void ChangeGameMode()
        {
            GameMode.SetMode(modeToSet);
        }
    }
}