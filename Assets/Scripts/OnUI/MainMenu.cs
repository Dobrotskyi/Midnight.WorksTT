using Scripts.Game.SaveSystem;
using Scripts.Managment;
using UnityEngine;

namespace Scripts.OnUI
{
    public class MainMenu : MonoBehaviour
    {
        public void LoadNewGame()
        {
            GameDataManager.Instance.NewGame();
            LoadGame();
        }

        public void LoadGame()
        {
            SceneLoading.LoadScene("Level");
        }

        public void CloseGame()
        {
            Application.Quit();
        }
    }
}