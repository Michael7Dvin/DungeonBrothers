using _Project.CodeBase.Gameplay.Characters;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Gameplay.Services.AI.Behaviours
{
    public interface IMoveBehaviour
    {
        public UniTask Move(ICharacter activeCharacter, ICharacter target);
    }
}