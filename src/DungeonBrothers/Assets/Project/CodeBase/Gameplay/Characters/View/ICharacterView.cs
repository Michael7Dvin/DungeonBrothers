using Project.CodeBase.Gameplay.Characters.View.Animators;
using Project.CodeBase.Gameplay.Characters.View.Hit;
using Project.CodeBase.Gameplay.Characters.View.Move;
using Project.CodeBase.Gameplay.Characters.View.Outline;
using Project.CodeBase.Gameplay.Characters.View.SpriteFlip;
using Project.CodeBase.UI.TurnQueue;

namespace Project.CodeBase.Gameplay.Characters.View
{
    public interface ICharacterView
    {
        public CharacterTurnQueueIcon Icon { get; }
        public ICharacterAnimator Animator { get; }
        public IMovementView MovementView { get; }
        public IHitView HitView { get; }
        public ISpriteFlip SpriteFlip { get; }
        public ICharacterOutline CharacterOutline { get; }
    }
}