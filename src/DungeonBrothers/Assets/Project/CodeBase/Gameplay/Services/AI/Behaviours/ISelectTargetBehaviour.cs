using Project.CodeBase.Gameplay.Characters;

namespace Project.CodeBase.Gameplay.Services.AI.Behaviours
{
    public interface ISelectTargetBehaviour
    {
        public ICharacter GetTarget();
    }
}