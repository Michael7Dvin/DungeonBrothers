using CodeBase.Infrastructure.Services.Providers.LevelSpawner;

namespace CodeBase.Infrastructure.Services.Providers.SceneServicesProvider
{
    public interface ISceneServicesProvider
    {
        ILevelSpawner LevelSpawner { get; }
        
        void SetLevelDataProvider(ILevelSpawner levelSpawner);
    }
}