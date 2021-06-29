using System;

namespace LevelSystem
{
    internal interface ILevelActions
    {
        event Action<int> LevelLoaded;
        event Action LevelCompleted;
        event Action NextLevelRequested;
    }
}