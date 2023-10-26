using _Project.CodeBase.Gameplay.Animations.Hit;
using _Project.CodeBase.UI.TurnQueue;

namespace _Project.CodeBase.Gameplay.Characters.View
{
    public class CharacterView : ICharacterView
    {
        public void Construct(CharacterInTurnQueueIcon characterInTurnQueueIcon,
            HitAnimation hitAnimation)
        {
            HitAnimation = hitAnimation;
            CharacterInTurnQueueIcon = characterInTurnQueueIcon;
        }
        
        public CharacterInTurnQueueIcon CharacterInTurnQueueIcon { get; private set;}
        public HitAnimation HitAnimation { get; private set; }
        
        public CharacterSounds CharacterSounds { get; private set; }
    }
}