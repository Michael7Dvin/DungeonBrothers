using UnityEngine;
using UnityEngine.Audio;

namespace _Project.CodeBase.Infrastructure.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour, ISoundPlayer
    {
        private AudioSource _audioSource;

        private void Start() => 
            _audioSource = GetComponent<AudioSource>();

        public void StartSoundtrack(AudioClip soundtrack)
        {
            _audioSource.clip = soundtrack;

            _audioSource.Play();
        }

        public void DisableSoundtrack() => 
            _audioSource.Stop();
    }
}