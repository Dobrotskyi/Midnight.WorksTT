using System;
using System.IO;
using UnityEngine;

namespace Scripts.Game.SaveSystem
{
    public class GameDataFileHandler
    {
        private const string FOLDER_NAME = "Savings";
        private string _fileName;

        private string FullPath => Path.Combine(Application.persistentDataPath, FOLDER_NAME, _fileName);
        private string DirectoryPath => Path.Combine(Application.persistentDataPath, FOLDER_NAME);

        public GameDataFileHandler(string fileName)
        {
            _fileName = fileName;
        }

        public GameData LoadFromFile()
        {
            GameData data = null;
            if (!File.Exists(FullPath))
                return data;

            string jsonData = string.Empty;
            using (FileStream stream = new(FullPath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    jsonData = reader.ReadToEnd();
                }
            }

            data = JsonUtility.FromJson<GameData>(jsonData);
            return data;
        }

        public void SaveToFile(GameData data)
        {
            try
            {
                Directory.CreateDirectory(DirectoryPath);
                string jsonData = JsonUtility.ToJson(data, true);
                using (FileStream stream = new(FullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(jsonData);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}