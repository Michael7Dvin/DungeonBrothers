using CodeBase.Common.FSM.States;
using CodeBase.Gameplay.Services.Map;
using CodeBase.Gameplay.Services.Move;
using CodeBase.Gameplay.Services.TurnQueue;
using CodeBase.Gameplay.Tiles;
using UnityEngine;

namespace CodeBase.Infrastructure.StateMachines.Gameplay.States
{
    public class BattleState : IState
    {
        private readonly ITurnQueue _turnQueue;
        private readonly IMoverService _moverService;
        private readonly IMapService _mapService;

        public BattleState(ITurnQueue turnQueue,
            IMoverService moverService,
            IMapService mapService)
        {
            _turnQueue = turnQueue;
            _moverService = moverService;
            _mapService = mapService;
        }

        public void Enter()
        {
            _moverService.Enable();
            _turnQueue.SetFirstTurn();
        }
            

        public void Exit()
        {
        }
    }
}