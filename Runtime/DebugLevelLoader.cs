using System;
using LevelSystem;
using UnityEngine.SceneManagement;

namespace LevelSystem.Loading
{
    internal class DebugLevelLoader : ILevelLoader
    {
        public void LoadLevel(int index, Action callback) => throw new NotImplementedException();
        
        public void ReloadLevel(Action callback)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
            callback?.Invoke();
        }

        public void LoadNextLevel(Action callback) => throw new NotImplementedException();
    }
}