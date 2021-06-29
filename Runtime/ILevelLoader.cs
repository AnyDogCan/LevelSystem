using System;

namespace LevelSystem.Loading
{
    internal interface ILevelLoader
    {
        public void LoadLevel(int index, Action callback);
        void ReloadLevel(Action callback);
        public void LoadNextLevel(Action callback);
    }
}