using UnityEngine.Events;

namespace DefaultNamespace
{
    public class GameMode
    {
        public enum Mode { Play, Paused }

        private static GameMode _instance;
        private static GameMode Instance => _instance ??= new GameMode();

        private Mode _currentMode;
        private UnityEvent<Mode> _currentModeChanged;

        public static Mode Current => Instance._currentMode;
        public static UnityEvent<Mode> ModeChanged => Instance._currentModeChanged;

        public GameMode()
        {
            _currentModeChanged = new UnityEvent<Mode>();
            _currentMode = Mode.Paused;
        }

        public static void SetMode(Mode mode)
        {
            Instance._currentMode = mode;
            Instance._currentModeChanged?.Invoke(mode);
        }
    }
}