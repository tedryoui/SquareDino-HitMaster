using UnityEngine.Events;

namespace DefaultNamespace
{
    public class PlayerMode
    {
        public enum Mode { Walk, Fight, Idle }

        private static PlayerMode _instance;
        private static PlayerMode Instance => _instance ??= new PlayerMode();

        private Mode _currentMode;
        private UnityEvent<Mode> _currentModeChanged;

        public static Mode Current => Instance._currentMode;
        public static UnityEvent<Mode> ModeChanged => Instance._currentModeChanged;

        public PlayerMode()
        {
            _currentModeChanged = new UnityEvent<Mode>();
            _currentMode = Mode.Walk;
        }

        public static void SetMode(Mode mode)
        {
            Instance._currentMode = mode;
            Instance._currentModeChanged?.Invoke(mode);
        }
    }
}