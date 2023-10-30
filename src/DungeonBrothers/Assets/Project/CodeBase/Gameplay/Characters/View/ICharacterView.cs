using Project.CodeBase.Gameplay.Characters.View.Hit;
using Project.CodeBase.Gameplay.Characters.View.Move;
using Project.CodeBase.Gameplay.Characters.View.Outline;
using Project.CodeBase.UI.TurnQueue;

namespace Project.CodeBase.Gameplay.Characters.View
{
    public interface ICharacterView
    {
        public CharacterInTurnQueueIcon Icon { get; }
        public IMovementView MovementView { get; }
        public IHitView HitView { get; }
        public CharacterOutline CharacterOutline { get; }
    }
}