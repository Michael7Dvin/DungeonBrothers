using Cysharp.Threading.Tasks;
using Infrastructure.Services.Logging;
using Infrastructure.StaticDataProviding;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.SceneLoading
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ScenesData _scenes;
        private readonly ICustomLogger _logger;

        public SceneLoader(IStaticDataProvider staticDataProvider, 
            ICustomLogger logger)
        {
            _scenes = staticDataProvider.ScenesData;
            _logger = logger;
        }

        public Scene CurrentScene { get; private set; }

        public async UniTask Load(SceneType type)
        {
            switch (type)
            {
                case SceneType.MainMenu:
                    await Load(_scenes.MainMenu);
                    break;
                case SceneType.Island:
                    await Load(_scenes.Island);
                    break;
                default:
                    _logger.LogError($"Unsupported {nameof(SceneType)}: '{type}'");
                    break;
            }    
        }

        private async UniTask Load(AssetReference sceneReference)
        {
            AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneReference);
            await handle.Task;
            CurrentScene = handle.Result.Scene;
        }
    }
}