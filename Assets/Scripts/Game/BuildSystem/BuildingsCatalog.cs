using Scripts.Game.Buildings;
using Scripts.Utils.Extensions;
using System.Collections.Generic;

namespace Scripts.Game
{
    public class BuildingsCatalog
    {
        private static BuildingsCatalog s_instance;
        public static BuildingsCatalog Instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = new();
                return s_instance;
            }
        }

        private Dictionary<string, BuildingSO> _catalog;
        private BuildingsCatalog()
        {
            _catalog = new();
            var buildingsData = ScriptableObjectExtensions<BuildingSO>.GetAllInstances();
            foreach (var data in buildingsData)
                _catalog.Add(data.Id, data);
        }

        public BuildingSO GetBuilding(string id)
        {
            _catalog.TryGetValue(id, out BuildingSO data);
            return data;
        }
    }
}