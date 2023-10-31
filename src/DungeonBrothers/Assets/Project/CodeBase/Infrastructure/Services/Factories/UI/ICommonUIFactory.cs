using Cysharp.Threading.Tasks;

namespace Project.CodeBase.Infrastructure.Services.Factories.UI
{
    public interface ICommonUIFactory
    {
        public UniTask WarmUp();
        public UniTask Create();
    }
}