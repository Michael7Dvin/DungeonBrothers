using System;

namespace CodeBase.Gameplay.Characters.Logic
{
    public class CharacterLogic : ICharacterLogic
    {
        public CharacterInjuring CharacterInjuring { get; private set; }
        
        public event Action Died;
    }
}