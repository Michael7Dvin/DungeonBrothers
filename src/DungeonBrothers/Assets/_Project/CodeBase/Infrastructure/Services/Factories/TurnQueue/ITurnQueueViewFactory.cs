using _Project.CodeBase.Gameplay.Characters.CharacterInfo;
using _Project.CodeBase.UI.TurnQueue;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace _Project.CodeBase.Infrastructure.Services.Factories.TurnQueue
{
    public interface ITurnQueueViewFactory
    {
        public UniTask WarmUp();
        public UniTask<CharacterInTurnQueueIcon> CreateIcon(AssetReferenceGameObject iconReference,
            CharacterID characterID);

        public UniTask CreateTurnQueueView();
    }
}