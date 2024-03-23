using UnityEngine;

namespace Scripts.Game.Buildings
{
    [CreateAssetMenu(menuName = "ScriptableObject/Building", fileName = "Building")]
    public class BuildingSO : ScriptableObject
    {
        [SerializeField] private Building _prefab;
        [SerializeField] private string _name;
        [TextArea]
        [SerializeField] private string _description;
        [SerializeField] private ProgressConfigSO _progressionConfig;
        [SerializeField] private string _id;

        public ProgressConfigSO ProgressConfig => _progressionConfig;
        public int PriceForBuild => ProgressConfig.PriceOnLevel(0);
        public string Name => _name;
        public string Description => _description;
        public string Id => _id;
        public Building Prefab => _prefab;

        [ContextMenu("Create id")]
        private void CreateId() => _id = System.Guid.NewGuid().ToString();
    }
}