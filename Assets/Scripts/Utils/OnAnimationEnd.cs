using UnityEngine;

namespace Scripts.Utils
{
    public class OnAnimationEnd : MonoBehaviour
    {
        [SerializeField] private bool _destroyOnEnd = false;
        public void OnAnimationEnded()
        {
            if (_destroyOnEnd)
                Destroy(gameObject);
            else
                gameObject.SetActive(false);
        }
    }
}