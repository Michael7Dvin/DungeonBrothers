using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Characters.CharacterInfo;
using Project.CodeBase.UI.TurnQueue;
using UnityEngine.AddressableAssets;

namespace Project.CodeBase.Infrastructure.Services.Factories.TurnQueue
{
    public interface ITurnQueueViewFactory
    {
        public UniTask WarmUp();
        public UniTask<CharacterInTurnQueueIcon> CreateIcon(AssetReferenceGameObject iconReference,
            CharacterID characterID);

        public UniTask CreateTurnQueueView();
    }
}