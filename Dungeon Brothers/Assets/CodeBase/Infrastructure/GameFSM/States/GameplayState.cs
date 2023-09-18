using CodeBase.Common.FSM.States;
using CodeBase.Infrastructure.Services.Factories.TileFactory;
using UnityEngine;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class GameplayState : IState
    {
        private readonly ITileFactory _tileFactory;

        public GameplayState(ITileFactory tileFactory)
        {
            _tileFactory = tileFactory;
        }

        public void Enter()
        {
            _tileFactory.Create(Vector3.zero, Vector2Int.zero);
        }

        public void Exit()
        {
        }
    }
}