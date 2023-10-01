using System;
using CodeBase.Gameplay.Characters.Logic;

namespace CodeBase.Gameplay.Characters.CharacterInfo
{
    public class CharacterLogic : ICharacterLogic
    {
        public event Action Died;
    }
}