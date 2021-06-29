using System;
using System.IO;
using System.Linq;
using LevelSystem.Data;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace LevelSystem.Editor
{
    internal class LevelSystemDataCreator : IPreprocessBuildWithReport
    {
        private static LevelSystemData _levelSystemData;
        
        static LevelSystemDataCreator()
        {
            LoadLevelSystemData();
            
            if (_levelSystemData == null)
                CreateLevelSystemData();
        }

        private static void CreateLevelSystemData()
        {
            //создавать в LevelSystem/Resources 
            LevelSystemData data = ScriptableObject.CreateInstance<LevelSystemData>();
            AssetDatabase.CreateAsset(data, Path.Combine(PathToResources, nameof(LevelSystemData) + ".asset"));

            LoadLevelSystemData();
        }

        private static void LoadLevelSystemData()
        {
            _levelSystemData = Resources.Load<LevelSystemData>(nameof(LevelSystemData));
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void UpdateLevelSystemData()
        {
            FindPathToLevelSettings();
            UpdateSceneNames();
        }

        private static void FindPathToLevelSettings()
        {
            string[] guids = AssetDatabase.FindAssets(nameof(LevelSystemSettings), new[] {PathToResources});
            
            if (!guids.Any())
                throw new Exception($"{nameof(LevelSystemSettings)} not founded. Create this via asset menu");

            if (guids.Length > 1)
                new Exception($"Multiple instances of {nameof(LevelSystemSettings)} found." +
                              "The first item found will be selected");

            string path = AssetDatabase.GUIDToAssetPath(guids.First());
            
            // from "Assets/Resources/Name.asset" to "Name" (for Resources.Load)
            path = path.Replace(PathToResources + '/', "");
            path = path.Substring(0, path.LastIndexOf('.'));

            _levelSystemData.PathToLevelSettings = path;
        }

        private static void UpdateSceneNames()
        {
            Levels levels = Resources.Load<Levels>(nameof(Levels));

            _levelSystemData.SceneNames = levels.Scenes.Select(sceneAsset => sceneAsset.name).ToArray();
            _levelSystemData.MainSceneName = levels.MainScene.name;
        }
        
        private const string PathToResources = "Assets/Resources";
        
        public int callbackOrder { get; }

        public void OnPreprocessBuild(BuildReport report)
        {
            UpdateLevelSystemData();
        }
    }
}