using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services.Factories.Buttons
{
    public interface IButtonsFactory
    {
        public UniTask WarmUp();

        public UniTask CreateSkipTurnButton();
    }
}