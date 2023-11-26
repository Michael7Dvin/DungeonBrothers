using Project.CodeBase.Gameplay.Characters.CharacterInfo;
using Project.CodeBase.Gameplay.Rooms.Doors;
using Project.CodeBase.Gameplay.Services.Map;
using Project.CodeBase.Gameplay.Services.Move;
using Project.CodeBase.Gameplay.Services.TurnQueue;
using Project.CodeBase.Infrastructure.Audio;
using Project.CodeBase.Infrastructure.Services.Providers.CharactersProvider;
using Project.CodeBase.Infrastructure.StateMachines.Common.States;
using UnityEngine;

namespace Project.CodeBase.Infrastructure.StateMachines.Gameplay.States
{

    public class IdleState : IState
    {
        private readonly ISoundPlayer _soundPlayer;
        private readonly IMoverService _moverService;
        private readonly ITurnQueue _turnQueue;
        private readonly IDoorSelector _doorSelector;
        private readonly ICharactersProvider _charactersProvider;
        private readonly IMapService _mapService;
        
        public IdleState(ISoundPlayer soundPlayer, 
            IMoverService moverService,
            ITurnQueue turnQueue,
            IDoorSelector doorSelector,
            ICharactersProvider charactersProvider,
            IMapService mapService)
        {
            _soundPlayer = soundPlayer;
            _moverService = moverService;
            _turnQueue = turnQueue;
            _doorSelector = doorSelector;
            _charactersProvider = charactersProvider;
            _mapService = mapService;
        }

        public void Enter()
        {
            SetIdleState();

            _doorSelector.Initialize();
            _moverService.Enable();
            _turnQueue.SetFirstTurn();
        }

        private void SetIdleState()
        {
            foreach (var character in _charactersProvider.GetAllCharacterFromID(CharacterID.Hero))
                character.IsInBattle = false;
        }

        public void Exit()
        {
            
        }
    }
}