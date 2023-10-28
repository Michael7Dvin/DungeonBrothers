using System;
using System.Collections.Generic;
using _Project.CodeBase.Infrastructure.Services.Logger;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace _Project.CodeBase.Gameplay.Characters.View.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class CharacterSounds : SerializedMonoBehaviour
    {
        private AudioSource _audioSource;
        private ICustomLogger _customLogger;

        [Inject]
        private void Inject(ICustomLogger customLogger) => 
            _customLogger = customLogger;

        [SerializeField] private Dictionary<CharacterSoundType, AudioClip> _audio;
        
        private void Start() => 
            _audioSource = GetComponent<AudioSource>();

        public void PlaySoundOneTime(CharacterSoundType characterSoundType)
        {
            if (_audio.TryGetValue(characterSoundType, out AudioClip audioClip))
                _audioSource.PlayOneShot(audioClip);
            else
                _customLogger.LogError(new Exception($"{characterSoundType}, doesn't have"));

        }

        public void PlaySoundInLoop(CharacterSoundType characterSoundType)
        {
            if (_audio.TryGetValue(characterSoundType, out AudioClip audioClip))
            {
                _audioSource.clip = audioClip;
                
                _audioSource.Play();
            }
            else
                _customLogger.LogError(new Exception($"{characterSoundType}, doesn't have"));
        }

        public void StopPlaySound() => 
            _audioSource.Stop();
    }
}