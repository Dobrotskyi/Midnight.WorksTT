using UnityEngine;

namespace Scripts.OnUI
{
    [RequireComponent(typeof(AudioSource))]
    public class ButtonClickSoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _as;

        private void OnEnable()
        {
            ButtonClickInvoke.ButtonClicked += PlaySound;
        }

        private void OnDisable()
        {
            ButtonClickInvoke.ButtonClicked -= PlaySound;
        }

        private void PlaySound()
        {
            _as.Play();
        }
    }
}