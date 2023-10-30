using Project.CodeBase.Gameplay.Characters.Logic.Deaths;
using Project.CodeBase.Gameplay.Characters.Logic.Healths;
using Project.CodeBase.Gameplay.Characters.Logic.Movement;

namespace Project.CodeBase.Gameplay.Characters.Logic
{
    public class CharacterLogic : ICharacterLogic
    {
        public CharacterLogic(IHealth health, IDeath death, IMovement movement)
        {
            Health = health;
            Death = death;
            Movement = movement;
        }

        public IHealth Health { get; }
        public IDeath Death { get; }
        public IMovement Movement { get; }
    }
}