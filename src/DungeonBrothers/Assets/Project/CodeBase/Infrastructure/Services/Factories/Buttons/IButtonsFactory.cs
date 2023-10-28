using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.Services.Factories.Buttons
{
    public interface IButtonsFactory
    {
        public UniTask WarmUp();

        public UniTask CreateSkipTurnButton();
    }
}