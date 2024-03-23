using Scripts.Game.SaveSystem;
using UnityEngine;

namespace Scripts.Game
{
    public class Bootstrap : MonoBehaviour
    {
        private IncomeCounter _counter;

        private void Awake()
        {
            GameDataManager.Instance.OnNewLevelLoaded();
            GameDataManager.Instance.LoadAll();
            _counter = new();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (!focus)
                GameDataManager.Instance.SaveToFile();
        }
    }
}