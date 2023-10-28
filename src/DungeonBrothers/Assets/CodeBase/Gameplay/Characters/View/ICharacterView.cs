using CodeBase.Gameplay.Animations.Hit;
using CodeBase.Gameplay.Characters.View.Outline;
using CodeBase.UI.TurnQueue;

namespace CodeBase.Gameplay.Characters.View
{
    public interface ICharacterView
    {
        public CharacterInTurnQueueIcon CharacterInTurnQueueIcon { get; }
        public HitAnimation HitAnimation { get; }
        CharacterOutline CharacterOutline { get; }
    }
}