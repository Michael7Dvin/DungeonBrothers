using CodeBase.Infrastructure.Services.Providers.LevelSpawner;

namespace CodeBase.Infrastructure.Services.Providers.SceneServicesProvider
{
    public class SceneServicesProvider : ISceneServicesProvider
    {
        public ILevelSpawner LevelSpawner { get; private set; }

        public void SetLevelDataProvider(ILevelSpawner levelSpawner) => 
            LevelSpawner = levelSpawner;
    }
}