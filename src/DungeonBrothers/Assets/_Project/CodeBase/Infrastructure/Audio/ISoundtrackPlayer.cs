using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.Audio
{
    public interface ISoundtrackPlayer
    {
        public void StartSoundtrack(AudioClip soundtrack);
        public void DisableSoundtrack();
    }
}