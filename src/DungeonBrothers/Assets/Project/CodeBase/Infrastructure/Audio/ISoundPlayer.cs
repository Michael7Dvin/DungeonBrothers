using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Audio
{
    public interface ISoundPlayer
    {
        public void StartSoundtrack(AudioClip soundtrack);
        public void DisableSoundtrack();
    }
}