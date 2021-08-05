using System;

namespace LevelSystem
{
    internal class LevelActions : ILevelActions
    {
        public event Action<int> LevelLoaded;
        public event Action LevelCompleted;
        public event Action NextLevelRequested;
        public event Action LevelFailed;

        public void OnLevelLoaded(int index) => LevelLoaded?.Invoke(index);
        
        public void OnLevelCompleted() => LevelCompleted?.Invoke();
        
        public void OnNextLevelRequested() => NextLevelRequested?.Invoke();
        
        public void OnLevelFailed() => LevelFailed?.Invoke();
    }
}