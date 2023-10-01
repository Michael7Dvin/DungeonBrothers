using System;

namespace CodeBase.Gameplay.Characters.Logic
{
    public interface ICharacterLogic
    {
        public event Action Died;
    }
}