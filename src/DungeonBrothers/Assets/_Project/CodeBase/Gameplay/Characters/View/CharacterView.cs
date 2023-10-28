using _Project.CodeBase.Gameplay.Characters.View.Move;
using _Project.CodeBase.UI.TurnQueue;

namespace _Project.CodeBase.Gameplay.Characters.View
{
    public class CharacterView : ICharacterView
    {
        public void Construct(CharacterInTurnQueueIcon characterInTurnQueueIcon,
            IMovementView movementView, 
            IHitView hitView)
        {
            Icon = characterInTurnQueueIcon;
            MovementView = movementView;
            HitView = hitView;
        }
        
        public CharacterInTurnQueueIcon Icon { get; private set;}
        public IMovementView MovementView { get; private set; }
        public IHitView HitView { get; private set; }
    }
}