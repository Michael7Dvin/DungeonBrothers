using CodeBase.Gameplay.Animations.Hit;
using CodeBase.UI.TurnQueue;

namespace CodeBase.Gameplay.Characters.View
{
    public class CharacterView : ICharacterView
    {
        public void Construct(CharacterInTurnQueueIcon characterInTurnQueueIcon,HitAnimation hitAnimation)
        {
            HitAnimation = hitAnimation;
            CharacterInTurnQueueIcon = characterInTurnQueueIcon;
        }
        
        public CharacterInTurnQueueIcon CharacterInTurnQueueIcon { get; private set;}
        public HitAnimation HitAnimation { get; private set; }
    }
}