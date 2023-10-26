using UnityEngine;

namespace _Project.CodeBase.Gameplay.Services.Audio
{
    public interface IAudioService
    {
        public void StartSoundtrack(AudioClip soundtrack);
        public void DisableSoundtrack();
        public void PlaySound(AudioClip sound);
    }
}