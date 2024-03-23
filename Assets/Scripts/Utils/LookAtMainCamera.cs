using UnityEngine;

namespace Scripts.Utils
{
    public class LookAtMainCamera : MonoBehaviour
    {
        [SerializeField] private bool _freezeX = false;
        [SerializeField] private bool _freezeY = false;
        [SerializeField] private bool _freezeZ = false;
        private Vector3 _defaultEulerRotation;

        private Camera _main;

        private void Awake()
        {
            _main = Camera.main;
            _defaultEulerRotation = transform.rotation.eulerAngles;
        }

        private void LateUpdate()
        {
            transform.LookAt(_main.transform);
            Vector3 currentRotation = transform.rotation.eulerAngles;
            if (_freezeX)
                currentRotation.x = _defaultEulerRotation.x;
            if (_freezeY)
                currentRotation.y = _defaultEulerRotation.y;
            if (_freezeZ)
                currentRotation.z = _defaultEulerRotation.z;
            transform.rotation = Quaternion.Euler(currentRotation);
        }
    }
}