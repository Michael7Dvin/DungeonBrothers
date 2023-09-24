using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Services.Providers.LevelSpawner
{
    public interface ILevelSpawner : IInitializable
    {
        public UniTask WarmUp();
        
        public UniTask Spawn();
    }
}