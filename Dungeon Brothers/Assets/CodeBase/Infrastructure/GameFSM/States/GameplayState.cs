using CodeBase.Common.FSM.States;
using CodeBase.Gameplay.Services.MapGenerator;

namespace CodeBase.Infrastructure.GameFSM.States
{
    public class GameplayState : IState
    {
        private readonly IMapGenerator _mapGenerator;

        public GameplayState(IMapGenerator mapGenerator)
        {
            _mapGenerator = mapGenerator;
        }

        public void Enter()
        {
            _mapGenerator.GenerateMap();
        }

        public void Exit()
        {
        }
    }
}