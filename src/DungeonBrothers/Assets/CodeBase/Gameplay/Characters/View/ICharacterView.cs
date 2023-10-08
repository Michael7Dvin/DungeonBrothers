using CodeBase.Gameplay.Animations.Hit;
using CodeBase.UI.TurnQueue;

namespace CodeBase.Gameplay.Characters.View
{
    public interface ICharacterView
    {
        public CharacterInTurnQueueIcon CharacterInTurnQueueIcon { get; }
        public HitAnimation HitAnimation { get; }
    }
}