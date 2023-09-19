using CodeBase.Gameplay.Characters;
using Cysharp.Threading.Tasks;

namespace CodeBase.Gameplay.UI.TurnQueue
{
    public interface ITurnQueueView
    {
        public void SubscribeToEvents();
        public void ReorganizeIcons(ICharacter character);
    }
}