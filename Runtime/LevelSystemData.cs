using System;
using System.Linq;
using LevelSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelSystem.Data
{
    // TODO перенести это в LevelSystemSettings
    public sealed class LevelSystemData : ScriptableObject
    {
        [SerializeField, HideInInspector] private string _pathToLevelSettings;
        [SerializeField, HideInInspector] private string[] _tutorialSceneNames;
        [SerializeField, HideInInspector] private string[] _sceneNames;
        [SerializeField, HideInInspector] private string _mainSceneName;

        public string PathToLevelSettings
        {
            get => _pathToLevelSettings;
            set => _pathToLevelSettings = value;
        }
        
        public string[] TutorialSceneNames
        {
            get => _tutorialSceneNames;
            set => _tutorialSceneNames = value;
        }

        public string[] SceneNames
        {
            get => _sceneNames;
            set => _sceneNames = value;
        }

        public string MainSceneName
        {
            get => _mainSceneName;
            set => _mainSceneName = value;
        }

        public string GetSceneByID(int index, LevelsLoopingType loopingType = LevelsLoopingType.None)
        {
            if (index < _tutorialSceneNames.Length)
            {
                return _tutorialSceneNames[index];
            }

            index -= _tutorialSceneNames.Length;

            int selectedIndex = NormalizeIndexByLoopingType(index, loopingType);
            
            if (selectedIndex >= _sceneNames.Length || _sceneNames[selectedIndex] == null)
                throw new Exception($"Scene with index {index} is not defined");
            
            return _sceneNames[selectedIndex];
        }

        public string GetRandomScene()
        {
            string[] availableScenes = _sceneNames.Where(name => !string.IsNullOrEmpty(name)).ToArray();

            if (!availableScenes.Any())
                throw new Exception("Not scenes available");

            int index = Random.Range(0, availableScenes.Length);
            
            return availableScenes.ElementAt(index);
        }

        private int NormalizeIndexByLoopingType(int index, LevelsLoopingType loopingType)
        {
            return loopingType switch
            {
                LevelsLoopingType.None => index,
                LevelsLoopingType.LoopLastLevel => index >= _sceneNames.Length ? _sceneNames.Length - 1 : index,
                LevelsLoopingType.RepeatAllLevels => index % _sceneNames.Length,
                _ => throw new ArgumentOutOfRangeException(nameof(loopingType), loopingType, null)
            };
        }
    }
}