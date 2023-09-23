using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services.Providers.LevelSpawner
{
    public interface ILevelSpawner
    {
        public UniTask WarmUp();
        public UniTask Spawn();
    }
}