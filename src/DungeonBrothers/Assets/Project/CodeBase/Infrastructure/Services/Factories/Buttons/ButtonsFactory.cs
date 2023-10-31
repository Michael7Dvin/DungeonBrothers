﻿using _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.UI.Gameplay;
using _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Loader;
using _Project.CodeBase.Infrastructure.Services.Factories.Buttons;
using _Project.CodeBase.Infrastructure.Services.StaticDataProvider;
using _Project.CodeBase.UI.Services.UIProvider;
using _Project.CodeBase.UI.TurnQueue.Button;
using Cysharp.Threading.Tasks;
using Project.CodeBase.UI.TurnQueue.Button;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.CodeBase.Infrastructure.Services.Factories.Buttons
{
    public class ButtonsFactory : IButtonsFactory
    {
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly IObjectResolver _objectResolver;
        private readonly IUIProvider _uiProvider;
        
        private readonly GameplayUIAddresses _gameplayUIAddresses;

        public ButtonsFactory(IAddressablesLoader addressablesLoader,
            IStaticDataProvider staticDataProvider,
            IUIProvider uiProvider,
            IObjectResolver objectResolver)
        {
            _addressablesLoader = addressablesLoader;
            _objectResolver = objectResolver;
            _uiProvider = uiProvider;
            
            _gameplayUIAddresses = staticDataProvider.AssetsAddresses.AllUIAddresses.GameplayUIAddresses;
        }


        public async UniTask WarmUp() => 
            await _addressablesLoader.LoadGameObject(_gameplayUIAddresses.SkipTurnButton);

        public async UniTask CreateSkipTurnButton()
        {
            Transform root = _uiProvider.Canvas.transform;
            
            GameObject prefab = await _addressablesLoader.LoadGameObject(_gameplayUIAddresses.SkipTurnButton);

            GameObject gameObject = _objectResolver.Instantiate(prefab, root);

            SkipTurnView skipTurnView = gameObject.GetComponent<SkipTurnView>();

            skipTurnView.Construct(CreateSkipTurnViewModel());
            
        }

        private SkipTurnViewModel CreateSkipTurnViewModel()
        {
            SkipTurnViewModel skipTurnViewModel = new SkipTurnViewModel();
            
            _objectResolver.Inject(skipTurnViewModel);

            return skipTurnViewModel;
        }
    }
}