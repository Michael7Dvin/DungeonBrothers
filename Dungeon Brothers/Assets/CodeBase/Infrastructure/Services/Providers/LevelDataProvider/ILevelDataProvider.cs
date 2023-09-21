using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Services.Providers.LevelDataProvider
{
    public interface ILevelDataProvider : IInitializable
    {
        public UniTask WarmUp();
        
        public UniTask CreateLevel();
    }
}