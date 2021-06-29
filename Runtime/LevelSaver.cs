using UnityEngine;

namespace LevelSystem
{
    internal static class LevelSaver
    {
        private const string LevelIndexPrefsKey = "LevelIndex";

        public static int GetCurrentLevelIndex() => PlayerPrefs.GetInt(LevelIndexPrefsKey);

        public static void IncrementCurrentLevelIndex()
        {
            PlayerPrefs.SetInt(LevelIndexPrefsKey, GetCurrentLevelIndex() + 1);
        }

        public static void UpdateCurrentLevelIndex(int index)
        {
            PlayerPrefs.SetInt(LevelIndexPrefsKey, index);
        }
    }
}