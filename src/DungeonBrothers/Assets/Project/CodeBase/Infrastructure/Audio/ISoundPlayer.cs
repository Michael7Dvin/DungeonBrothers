using UnityEngine;

namespace Project.CodeBase.Infrastructure.Audio
{
    public interface ISoundPlayer
    {
        public void StartSoundtrack(AudioClip soundtrack);
        public void DisableSoundtrack();
    }
}