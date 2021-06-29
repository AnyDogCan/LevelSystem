using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LevelSystem.Editor
{
    [CreateAssetMenu(fileName = "Levels", menuName = "LevelSystem/Levels")]
    public sealed class Levels : ScriptableObject
    {
        [SerializeField] private SceneAsset _mainScene;
        [SerializeField] private List<SceneAsset> _scenes;

        public SceneAsset MainScene => _mainScene;
        public List<SceneAsset> Scenes => _scenes;
    }
}
