using Cysharp.Threading.Tasks;
using Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses;
using Project.CodeBase.Infrastructure.Services.AddressablesLoader.Loader;
using Project.CodeBase.Infrastructure.Services.Providers.CameraProvider;
using Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.CodeBase.Infrastructure.Services.Factories.Cameras
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

        public async UniTask<Camera> Create()
        {
            Camera camera = await _addressablesLoader.LoadComponent<Camera>(_addresses.Camera);

            Camera gameObject = _objectResolver.Instantiate(camera);
            _cameraProvider.SetCamera(gameObject);

            return gameObject;
        }
    }
}