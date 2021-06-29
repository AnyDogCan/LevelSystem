using UnityEngine;

namespace LevelSystem.Data
{
    [CreateAssetMenu(fileName = "LevelSystemSettings", menuName = "LevelSystem/LevelSystemSettings")]
    public class LevelSystemSettings : ScriptableObject
    {
        [SerializeField] private LevelsLoopingType _loopingType = LevelsLoopingType.RepeatAllLevels;

        public LevelsLoopingType LoopingType => _loopingType;
    }
}