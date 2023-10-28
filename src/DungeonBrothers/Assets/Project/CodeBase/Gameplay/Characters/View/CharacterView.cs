using _Project.CodeBase.Gameplay.Characters.View.Hit;
using _Project.CodeBase.Gameplay.Characters.View.Move;
using _Project.CodeBase.Gameplay.Characters.View.Outline;
using _Project.CodeBase.UI.TurnQueue;

namespace _Project.CodeBase.Gameplay.Characters.View
{
    public class CharacterView : ICharacterView
    {
        public void Construct(CharacterInTurnQueueIcon characterInTurnQueueIcon,
            IMovementView movementView, 
            IHitView hitView,
            CharacterOutline characterOutline)
        {
            Icon = characterInTurnQueueIcon;
            MovementView = movementView;
            HitView = hitView;
            CharacterOutline = characterOutline;
        }
        
        public CharacterInTurnQueueIcon Icon { get; private set;}
        public IMovementView MovementView { get; private set; }
        public IHitView HitView { get; private set; }
        public CharacterOutline CharacterOutline { get; private set; }
    }
}