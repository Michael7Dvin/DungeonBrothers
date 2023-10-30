using Project.CodeBase.Gameplay.Characters.Logic.Deaths;
using Project.CodeBase.Gameplay.Characters.Logic.Healths;
using Project.CodeBase.Gameplay.Characters.Logic.Movement;

namespace Project.CodeBase.Gameplay.Characters.Logic
{
    public interface ICharacterLogic
    {
        public IHealth Health { get; }
        public IDeath Death { get; }
        public IMovement Movement { get; }
    }
}