using System;

namespace LevelSystem
{
    public interface ILevelActions
    {
        event Action<int> LevelLoaded;
        event Action LevelCompleted;
        event Action NextLevelRequested;
    }
}