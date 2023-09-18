using CodeBase.Gameplay.UI.TurnQueue;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services.Factories.TurnQueue
{
    public interface ITurnQueueViewFactory
    {
        public UniTask WarmUp();
        public UniTask<CharacterInTurnQueueIcon> Create(AssetReferenceGameObject iconReference);
    }
}