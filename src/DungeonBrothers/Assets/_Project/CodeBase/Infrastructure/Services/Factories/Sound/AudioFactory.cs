using _Project.CodeBase.Gameplay.Services.Audio;
using _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Loader;
using _Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace _Project.CodeBase.Infrastructure.Services.Factories.Sound
{
    public class AudioFactory : IAudioFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly IAudioService _audioService;

        private readonly AssetReferenceGameObject _audioSource;

        public AudioFactory(IObjectResolver objectResolver,
            IAddressablesLoader addressablesLoader,
            IAudioService audioService,
            IStaticDataProvider staticDataProvider)
        {
            _objectResolver = objectResolver;
            _addressablesLoader = addressablesLoader;
            _audioService = audioService;
            
            _audioSource = staticDataProvider.AssetsAddresses.AllGameplayAddresses.SoundAddresses.SoundSource;
        }

        public async UniTask WarmUp()
        {
            await _addressablesLoader.LoadComponent<AudioSource>(_audioSource);
        }

        public async UniTask Create()
        {
            AudioSource gameObject = await _addressablesLoader.LoadComponent<AudioSource>(_audioSource);
            
            AudioSource prefab = _objectResolver.Instantiate(gameObject);
            _audioService.Construct(prefab);
        }
    }
}