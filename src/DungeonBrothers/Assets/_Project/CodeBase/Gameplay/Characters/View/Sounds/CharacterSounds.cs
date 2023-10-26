using UnityEngine;

namespace _Project.CodeBase.Gameplay.Characters.View
{
    public class CharacterSounds : MonoBehaviour
    {
        private AudioSource _audioSource;
        private AudioClip _audio;
        
        
        public void Play(SoundType soundType)
        {
            _audioSource.clip = _audio;
        }

    }
}