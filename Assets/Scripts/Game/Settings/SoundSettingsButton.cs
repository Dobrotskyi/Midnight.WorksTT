using Scripts.Managment;
using UnityEngine;

namespace Scripts.OnUI
{
    public class SoundSettingsButton : MonoBehaviour
    {
        [SerializeField] private VolumeSettings _settings;
        [SerializeField] private ButtonWithToggle _toggle;

        public enum Type
        {
            Sound,
            Music
        }
        [SerializeField] private Type _type;

        public void ToggleSetting()
        {
            if (_type == Type.Sound)
                _settings.ToggleSound();
            else
                _settings.ToggleMusic();
        }

        private void OnEnable()
        {
            UpdateRepresentation();
            _settings.SettingChanged += UpdateRepresentation;
        }

        private void OnDisable()
        {
            _settings.SettingChanged -= UpdateRepresentation;
        }

        private void UpdateRepresentation() => _toggle.On = _type == Type.Sound ? !_settings.SoundMuted : !_settings.MusicMuted;
    }

}