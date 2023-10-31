using Cysharp.Threading.Tasks;

namespace Project.CodeBase.Infrastructure.Services.Providers.LevelSpawner
{
    public interface ILevelSpawner
    {
        public UniTask WarmUp();
        public UniTask Spawn();
    }
}