using LevelSystem.Data;
using LevelSystem.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelSystem
{
    public static class LevelProcessor
    {
        private static readonly LevelSystemData _levelSystemData;
        private static readonly LevelActions _levelActions;
        
        private static ILevelLoader _loader;

        public static int CurrentIndex => LevelSaver.GetCurrentLevelIndex();
        public static ILevelActions LevelActions => _levelActions;

        static LevelProcessor()
        {
            _levelActions = new LevelActions();
            
            _levelSystemData = Resources.Load<LevelSystemData>(nameof(LevelSystemData));

            LevelSystemSettings settings = Resources.Load<LevelSystemSettings>(_levelSystemData.PathToLevelSettings);
            _loader = new LevelLoader(settings);
        }
        
        public static void RequestNextLevel()
        {
            _levelActions.OnNextLevelRequested();

            _loader.LoadNextLevel(OnSceneLoaded);
        }
        
        public static void ReloadLevel() => _loader.ReloadLevel(OnSceneLoaded);

        public static void LoadPreviousLevel() => _loader.LoadPreviousLevel(OnSceneLoaded);

        public static void CompleteLevel() => _levelActions.OnLevelCompleted();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void CheckFirstScene()
        {
            if (_levelSystemData.MainSceneName != SceneManager.GetActiveScene().name)
            {
                Debug.LogWarning("Main scene skipped. Debug mode activated");

                _loader = new DebugLevelLoader();
            }
        }
        
        private static void LoadCurrentLevel() => LoadLevel(LevelSaver.GetCurrentLevelIndex());
        
        private static void LoadLevel(int index) => _loader.LoadLevel(index, OnSceneLoaded);

        private static void OnSceneLoaded()
        {
            Debug.Log($"On scene loaded. Current level index {CurrentIndex}");
            
            _levelActions.OnLevelLoaded(CurrentIndex);
        }
    }
}