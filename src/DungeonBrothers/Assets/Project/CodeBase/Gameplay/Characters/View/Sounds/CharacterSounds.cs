using System;
using System.Collections.Generic;
using Project.CodeBase.Infrastructure.Services.Logger;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Project.CodeBase.Gameplay.Characters.View.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class CharacterSounds : SerializedMonoBehaviour, ICharacterSounds
    {
        [SerializeField] private Dictionary<CharacterSoundType, AudioClip> _audioClips;
        
        private AudioSource _audioSource;
        private ICustomLogger _customLogger;
        
        [Inject]
        private void Inject(ICustomLogger customLogger) => 
            _customLogger = customLogger;

        private void Start() => 
            _audioSource = GetComponent<AudioSource>();

        public void PlaySoundOneTime(CharacterSoundType characterSoundType)
        {
            if (IsHaveAudioClips() == false)
                return;
            
            if (_audioClips.TryGetValue(characterSoundType, out AudioClip audioClip))
                _audioSource.PlayOneShot(audioClip);
            else
                _customLogger.Log(new Exception($"{characterSoundType}, doesn't have"));
        }

        public void PlaySoundInLoop(CharacterSoundType characterSoundType)
        {
            if (IsHaveAudioClips() == false)
                return;
            
            if (_audioClips.TryGetValue(characterSoundType, out AudioClip audioClip))
            {
                _audioSource.clip = audioClip;
                _audioSource.Play();
            }
            else
                _customLogger.Log(new Exception($"{characterSoundType}, doesn't have"));
        }

        private bool IsHaveAudioClips()
        {
            if (_audioClips == null)
            {
                _customLogger.Log(new Exception($"He doesn't have any sounds"));
                return false;
            }

            return true;
        }
            

        public void StopPlaySound() => 
            _audioSource.Stop();
    }
}