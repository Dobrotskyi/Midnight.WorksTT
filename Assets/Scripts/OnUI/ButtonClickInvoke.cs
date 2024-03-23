using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.OnUI
{
    [RequireComponent(typeof(Button))]
    public class ButtonClickInvoke : MonoBehaviour
    {
        public static event Action ButtonClicked;
        private Button _button;

        private void OnEnable()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Invoke);
        }

        private void OnDisable() => _button.onClick.RemoveListener(Invoke);

        private void Invoke()
        {
            ButtonClicked?.Invoke();
        }

    }
}