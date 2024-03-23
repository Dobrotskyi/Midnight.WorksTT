using UnityEngine;

namespace Scripts.OnUI
{
    public class ButtonWithToggle : MonoBehaviour
    {
        [SerializeField] private GameObject _iconOn;
        [SerializeField] private GameObject _iconOff;

        public bool On
        {
            get => _iconOff.activeSelf;

            set
            {
                _iconOn.SetActive(value);
                _iconOff.SetActive(!value);
            }
        }
    }

}