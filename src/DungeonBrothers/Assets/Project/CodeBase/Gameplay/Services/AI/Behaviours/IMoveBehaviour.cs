using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Characters;

namespace Project.CodeBase.Gameplay.Services.AI.Behaviours
{
    public interface IMoveBehaviour
    {
        public UniTask Move(ICharacter activeCharacter, ICharacter target);
    }
}