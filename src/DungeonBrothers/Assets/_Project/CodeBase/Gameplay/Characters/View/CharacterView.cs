using _Project.CodeBase.Gameplay.Animations.Hit;
using _Project.CodeBase.Gameplay.Characters.View.Move;
using _Project.CodeBase.Gameplay.Characters.View.Sounds;
using _Project.CodeBase.UI.TurnQueue;

namespace _Project.CodeBase.Gameplay.Characters.View
{
    public class CharacterView : ICharacterView
    {
        public void Construct(CharacterInTurnQueueIcon characterInTurnQueueIcon,
            HitAnimation hitAnimation)
        {
            HitAnimation = hitAnimation;
            Icon = characterInTurnQueueIcon;
        }
        
        public CharacterInTurnQueueIcon Icon { get; private set;}
        public HitAnimation HitAnimation { get; private set; }
        public MovementView MovementView { get; private set; }
        public HitView HitView { get; private set; }
    }
}