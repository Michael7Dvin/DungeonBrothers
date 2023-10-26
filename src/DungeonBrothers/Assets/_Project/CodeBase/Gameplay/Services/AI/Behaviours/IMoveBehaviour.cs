using CodeBase.Gameplay.Characters;
using Cysharp.Threading.Tasks;

namespace CodeBase.Gameplay.Services.AI.Behaviours
{
    public interface IMoveBehaviour
    {
        public UniTask Move(ICharacter activeCharacter, ICharacter target);
    }
}