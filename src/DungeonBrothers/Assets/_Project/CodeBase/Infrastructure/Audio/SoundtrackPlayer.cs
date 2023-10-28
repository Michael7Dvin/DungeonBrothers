using _Project.CodeBase.Infrastructure.Services.Audio;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundtrackPlayer : MonoBehaviour, ISoundtrackPlayer
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