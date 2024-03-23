using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Scripts.Managment
{
    public class VolumeSettings : MonoBehaviour
    {
        private const float MUTED = -80f;

        public event Action SettingChanged;
        [SerializeField] private AudioMixer _mixer;

        public bool MusicMuted
        {
            get
            {
                float volume;
                _mixer.GetFloat("MusicVolume", out volume);
                return volume == MUTED;
            }
        }

        public bool SoundMuted
        {
            get
            {
                float volume;
                _mixer.GetFloat("SFXVolume", out volume);
                return volume == MUTED;
            }
        }

        public void ToggleMusic() => ToggleSetting("MusicVolume");

        public void ToggleSound() => ToggleSetting("SFXVolume");

        private void ToggleSetting(string name)
        {
            float current;
            _mixer.GetFloat(name, out current);
            if (current == 0f)
                _mixer.SetFloat(name, MUTED);
            else
                _mixer.SetFloat(name, 0f);
            SettingChanged?.Invoke();
        }
    }
}