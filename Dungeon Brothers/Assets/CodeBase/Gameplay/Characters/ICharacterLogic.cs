using System;

namespace CodeBase.Gameplay.Characters
{
    public interface ICharacterLogic
    {
        public event Action Died;
    }
}