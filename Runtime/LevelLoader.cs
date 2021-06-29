using System;
using LevelSystem.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelSystem.Loading
{
    internal class LevelLoader : ILevelLoader
    {
        private readonly LevelSystemData _levelSystemData;
        private readonly LevelsLoopingType _loopingType;
        
        public LevelLoader(LevelSystemSettings settings)
        {
            _levelSystemData = Resources.Load<LevelSystemData>(nameof(LevelSystemData));
            _loopingType = settings.LoopingType;
        }

        public void LoadLevel(int index, Action callback)
        {
            LevelSaver.UpdateCurrentLevelIndex(index);
            
            Load(_levelSystemData.GetSceneByID(index, _loopingType), callback);
        }
        
        public void ReloadLevel(Action callback) => LoadLevel(LevelSaver.GetCurrentLevelIndex(), callback);

        public void LoadNextLevel(Action callback) => LoadLevel(LevelSaver.GetCurrentLevelIndex() + 1, callback);
        
        private void Load(string scene, Action callback)
        {
            SceneManager.LoadScene(scene);
            
            callback?.Invoke();
        }
    }
}