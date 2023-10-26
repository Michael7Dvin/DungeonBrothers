using UnityEngine;

namespace _Project.CodeBase.Gameplay.Services.Audio
{
    public class AudioService : IAudioService
    {
        private AudioSource _audioSource;
        private AudioClip _currentSoundtrack;

        public void Construct(AudioSource audioSource) => 
            _audioSource = audioSource;

        public void StartSoundtrack(AudioClip soundtrack)
        {
            _audioSource.clip = soundtrack;
            
            _audioSource.Play();
        }

        public void DisableSoundtrack() => 
            _audioSource.Stop();

        public void PlaySound(AudioClip sound) => 
            _audioSource.PlayOneShot(sound);
    }
}