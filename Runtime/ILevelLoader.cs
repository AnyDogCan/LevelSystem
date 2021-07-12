using System;

namespace LevelSystem.Loading
{
    internal interface ILevelLoader
    {
        void LoadLevel(int index, Action callback);
        void ReloadLevel(Action callback);
        void LoadNextLevel(Action callback);
        void LoadPreviousLevel(Action callback);
    }
}