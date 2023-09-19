using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services.Factories.Common
{
    public interface ICommonUIFactory
    {
        public UniTask WarmUp();
        public UniTask Create();
    }
}