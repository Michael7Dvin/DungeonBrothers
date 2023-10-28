using _Project.CodeBase.Gameplay.Characters.View.Hit;
using _Project.CodeBase.Gameplay.Characters.View.Move;
using _Project.CodeBase.Gameplay.Characters.View.Outline;
using _Project.CodeBase.UI.TurnQueue;

namespace _Project.CodeBase.Gameplay.Characters.View
{
    public interface ICharacterView
    {
        public CharacterInTurnQueueIcon Icon { get; }
        public IMovementView MovementView { get; }
        public IHitView HitView { get; }
        public CharacterOutline CharacterOutline { get; }
    }
}