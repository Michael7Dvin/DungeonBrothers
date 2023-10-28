using _Project.CodeBase.Gameplay.Services.Move;
using _Project.CodeBase.Gameplay.Services.TurnQueue;
using _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Loader;
using _Project.CodeBase.Infrastructure.Services.Audio;
using _Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using _Project.CodeBase.Infrastructure.StateMachines.Common.States;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.CodeBase.Infrastructure.StateMachines.Gameplay.States
{
    public class BattleState : IState
    {
        private readonly ITurnQueue _turnQueue;
        private readonly IMoverService _moverService;
        private readonly ISoundPlayer _soundPlayer;
        private readonly IAddressablesLoader _addressablesLoader;

        private AssetReference _audio;

        public BattleState(ITurnQueue turnQueue, 
            IMoverService moverService,
            ISoundPlayer soundPlayer,
            IAddressablesLoader addressablesLoader,
            IStaticDataProvider staticDataProvider)
        {
            _turnQueue = turnQueue;
            _moverService = moverService;
            _soundPlayer = soundPlayer;
            _addressablesLoader = addressablesLoader;
            _audio = staticDataProvider.AssetsAddresses.AllGameplayAddresses.SoundAddresses.DungeonSoundtrack;
        }

        public async void Enter()
        {
            AsyncOperationHandle<AudioClip> async = Addressables.LoadAssetAsync<AudioClip>(_audio);

            await async.Task;
            
            _soundPlayer.StartSoundtrack(async.Result);
            
            _moverService.Enable();
            _turnQueue.SetFirstTurn();
        }
            

        public void Exit()
        {
        }
    }
}