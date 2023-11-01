namespace Project.CodeBase.Gameplay.Characters.View.Sounds
{
    public interface ICharacterSounds
    {
        void PlaySoundOneTime(CharacterSoundType characterSoundType);
        void PlaySoundInLoop(CharacterSoundType characterSoundType);
        void StopPlaySound();
    }
}