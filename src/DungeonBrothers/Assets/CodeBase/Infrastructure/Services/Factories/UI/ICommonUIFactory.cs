using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services.Factories.UI
{
    public interface ICommonUIFactory
    {
        public UniTask WarmUp();
        public UniTask Create();
    }
}