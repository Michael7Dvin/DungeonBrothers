using CodeBase.Gameplay.Characters;

namespace CodeBase.Gameplay.UI.TurnQueue
{
    public interface ITurnQueueView
    {
        public void SubscribeToEvents();
        public void ReorganizeIcons(ICharacter character);
    }
}