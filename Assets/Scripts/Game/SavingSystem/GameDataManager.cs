using Scripts.Managment;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Game.SaveSystem
{
    public class GameDataManager
    {
        private static GameDataManager s_instance;
        public static GameDataManager Instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = new();
                return s_instance;
            }
        }

        private List<IPersistentData> _persistentDataObjects;
        private GameData _data;
        private GameDataFileHandler _handler;

        private GameDataManager()
        {
            _handler = new("save.json");
            _data = null;
            SceneLoading.ChangingScene += OnSceneChanged;
            FindAllMBObjects();
        }

        ~GameDataManager()
        {
            SceneLoading.ChangingScene -= OnSceneChanged;
        }

        public void OnNewLevelLoaded()
        {
            _persistentDataObjects.Clear();
            FindAllMBObjects();
        }

        public void LoadAll()
        {
            LoadOrCreateData();
            foreach (var obj in _persistentDataObjects)
                obj.LoadDataFrom(_data);
        }

        public void LoadData(IPersistentData obj)
        {
            LoadOrCreateData();
            obj.LoadDataFrom(_data);
        }

        public void SaveAllData() => SaveData(_persistentDataObjects);

        public void SaveData(IEnumerable<IPersistentData> objs)
        {
            foreach (var obj in objs)
                obj.SaveDataTo(_data);
        }

        public void SaveToFile() => _handler.SaveToFile(_data);

        public void NewGame() => _data = new();

        private void OnSceneChanged()
        {
            if (_persistentDataObjects != null && _persistentDataObjects.Count > 0)
            {
                SaveAllData();
                SaveToFile();
                _persistentDataObjects.Clear();
            }
        }

        private void LoadOrCreateData()
        {
            if (_data != null)
                return;

            _data = _handler.LoadFromFile();
            if (_data == null)
                NewGame();
        }

        private void FindAllMBObjects()
        {
            var objects = Object.FindObjectsOfType<MonoBehaviour>().
                                     OfType<IPersistentData>();
            _persistentDataObjects = objects.ToList();
        }
    }
}