using System.IO;
using UnityEngine;

namespace Framework.Data
{
    public static class Save
    {
        #region Functions

        public static void SaveGame(GameData gameData, string filePath, string fileName)
        {
            if (gameData != null)
            {
                string jsonData = JsonUtility.ToJson(gameData, false);
                File.WriteAllText(filePath + fileName, jsonData);
            }
            else
                Debug.Log("GameData is null. Could not be saved game data!");
        }

        #endregion Functions
    }
}