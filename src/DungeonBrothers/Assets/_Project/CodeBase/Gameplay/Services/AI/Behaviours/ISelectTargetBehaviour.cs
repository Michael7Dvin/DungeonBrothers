using CodeBase.Gameplay.Characters;

namespace CodeBase.Gameplay.Services.AI.Behaviours
{
    public interface ISelectTargetBehaviour
    {
        public ICharacter GetTarget();
    }
}