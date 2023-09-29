using System;

namespace CodeBase.Gameplay.Characters
{
    public class CharacterLogic : ICharacterLogic
    {
        public event Action Died;
    }
}