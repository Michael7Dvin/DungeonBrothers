using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.Services.Providers.LevelSpawner
{
    public interface ILevelSpawner
    {
        public UniTask WarmUp();
        public UniTask Spawn();
    }
}