using System;
using CodeBase.Infrastructure.Services.Logger;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Services.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ScenesAddresses _scenes;
        private readonly ICustomLogger _logger;

        public SceneLoader(IStaticDataProvider staticDataProvider, 
            ICustomLogger logger)
        {
            _scenes = staticDataProvider.AssetsAddresses.Scenes;
            _logger = logger;
        }

        public Scene CurrentScene { get; private set; }

        public async UniTask Load(SceneType type)
        {
            switch (type)
            {
                case SceneType.Level:
                    await Load(_scenes.Level);
                    break;
                default:
                    _logger.LogError(new Exception($"Unsupported {nameof(SceneType)}: '{type}'"));
                    break;
            }    
            
            _logger.Log($"Load scene: '{type}'");
        }

        private async UniTask Load(AssetReference sceneReference)
        {
            AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneReference);
            await handle.Task;
            CurrentScene = handle.Result.Scene;
        }
    }
}