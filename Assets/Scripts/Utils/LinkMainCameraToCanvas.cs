using UnityEngine;

namespace Scripts.Utils
{
    [RequireComponent(typeof(Canvas))]
    public class LinkMainCameraToCanvas : MonoBehaviour
    {
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.worldCamera = Camera.main;
        }
    }
}