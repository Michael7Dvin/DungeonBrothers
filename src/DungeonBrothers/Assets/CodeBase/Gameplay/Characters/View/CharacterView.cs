using CodeBase.Gameplay.Animations.Hit;
using CodeBase.UI.TurnQueue;

namespace CodeBase.Gameplay.Characters.View
{
    public class CharacterView : ICharacterView
    {
        public void Construct(CharacterInTurnQueueIcon characterInTurnQueueIcon,
            HitAnimation hitAnimation,
            CharacterOutline characterOutline)
        {
            HitAnimation = hitAnimation;
            CharacterInTurnQueueIcon = characterInTurnQueueIcon;
            CharacterOutline = characterOutline;
        }
        
        public CharacterInTurnQueueIcon CharacterInTurnQueueIcon { get; private set;}
        public HitAnimation HitAnimation { get; private set; }
        public CharacterOutline CharacterOutline { get; private set; }
    }
}