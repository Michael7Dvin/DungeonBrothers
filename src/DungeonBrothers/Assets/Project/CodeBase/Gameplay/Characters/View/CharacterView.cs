using Project.CodeBase.Gameplay.Characters.View.Hit;
using Project.CodeBase.Gameplay.Characters.View.Move;
using Project.CodeBase.Gameplay.Characters.View.Outline;
using Project.CodeBase.Gameplay.Characters.View.SpriteFlip;
using Project.CodeBase.UI.TurnQueue;

namespace Project.CodeBase.Gameplay.Characters.View
{
    public class CharacterView : ICharacterView
    {
        public void Construct(CharacterTurnQueueIcon characterTurnQueueIcon,
            IMovementView movementView, 
            IHitView hitView,
            ISpriteFlip spriteFlip,
            ICharacterOutline characterOutline)
        {
            Icon = characterTurnQueueIcon;
            MovementView = movementView;
            HitView = hitView; 
            SpriteFlip = spriteFlip;
            CharacterOutline = characterOutline;
        }
        
        public CharacterTurnQueueIcon Icon { get; private set;}
        public IMovementView MovementView { get; private set; }
        public IHitView HitView { get; private set; }
        public ISpriteFlip SpriteFlip { get; private set; }
        public ICharacterOutline CharacterOutline { get; private set; }
    }
}