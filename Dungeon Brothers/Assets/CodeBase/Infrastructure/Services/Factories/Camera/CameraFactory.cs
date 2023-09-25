using CodeBase.Infrastructure.Services.AddressablesLoader.Addresses;
using CodeBase.Infrastructure.Services.AddressablesLoader.Loader;
using CodeBase.Infrastructure.Services.Providers.CameraProvider;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Services.Factories.Camera
{
    public class CameraFactory : ICameraFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly ICameraProvider _cameraProvider;
        
        private readonly AllAssetsAddresses _addresses;

        public CameraFactory(IObjectResolver objectResolver, 
            IAddressablesLoader addressablesLoader,
            ICameraProvider cameraProvider,
            IStaticDataProvider staticDataProvider)
        {
            _objectResolver = objectResolver;
            _addressablesLoader = addressablesLoader;
            _cameraProvider = cameraProvider;
            
            _addresses = staticDataProvider.AssetsAddresses;
        }

        public async UniTask WarmUp()
        {
            await _addressablesLoader.LoadGameObject(_addresses.Camera);
        }

        public async UniTask<UnityEngine.Camera> Create()
        {
            UnityEngine.Camera camera = await _addressablesLoader.LoadComponent<UnityEngine.Camera>(_addresses.Camera);

            UnityEngine.Camera gameObject = _objectResolver.Instantiate(camera);
            _cameraProvider.SetCamera(gameObject);

            return gameObject;
        }
    }
}