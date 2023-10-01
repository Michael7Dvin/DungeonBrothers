using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Characters.CharacterInfo;
using CodeBase.UI.TurnQueue;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Services.Factories.TurnQueue
{
    public interface ITurnQueueViewFactory
    {
        public UniTask WarmUp();
        public UniTask<CharacterInTurnQueueIcon> CreateIcon(AssetReferenceGameObject iconReference,
            CharacterID characterID);

        public UniTask CreateTurnQueueView();
    }
}