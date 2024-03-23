using TMPro;
using UnityEngine;

namespace Scripts.OnUI
{
    public class PopUpScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleField;
        [SerializeField] private TextMeshProUGUI _descriptionField;

        public void Init(string title, string description)
        {
            _titleField.text = title;
            _descriptionField.text = description;
        }

        public void InitFailed() => Init("Ooops", "Not enough coins");
    }
}