using _Project.CodeBase.Gameplay.Animations.Hit;
using _Project.CodeBase.UI.TurnQueue;

namespace _Project.CodeBase.Gameplay.Characters.View
{
    public interface ICharacterView
    {
        public CharacterInTurnQueueIcon CharacterInTurnQueueIcon { get; }
        public HitAnimation HitAnimation { get; }
    }
}