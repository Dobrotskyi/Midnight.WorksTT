using UnityEngine;

namespace Scripts.Game
{
    public abstract class Menu : MonoBehaviour
    {
        [SerializeField] private GameObject _body;

        public void Open() => _body.SetActive(true);
        public void Close() => _body.GetComponent<Animator>().SetTrigger("Close");
    }
}