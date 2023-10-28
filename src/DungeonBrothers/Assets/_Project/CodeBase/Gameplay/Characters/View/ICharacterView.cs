using _Project.CodeBase.Gameplay.Animations.Hit;
using _Project.CodeBase.Gameplay.Characters.View.Move;
using _Project.CodeBase.Gameplay.Characters.View.Sounds;
using _Project.CodeBase.UI.TurnQueue;

namespace _Project.CodeBase.Gameplay.Characters.View
{
    public interface ICharacterView
    {
        public CharacterInTurnQueueIcon Icon { get; }
        public HitAnimation HitAnimation { get; }
        public MovementView MovementView { get; }
    }
}