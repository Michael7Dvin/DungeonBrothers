using _Project.CodeBase.Gameplay.Characters;

namespace _Project.CodeBase.Gameplay.Services.AI.Behaviours
{
    public interface ISelectTargetBehaviour
    {
        public ICharacter GetTarget();
    }
}