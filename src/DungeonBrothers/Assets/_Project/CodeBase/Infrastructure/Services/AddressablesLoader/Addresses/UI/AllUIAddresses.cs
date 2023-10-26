﻿using _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.UI.Common;
using _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.UI.Gameplay;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.UI
{
    [CreateAssetMenu(menuName = "StaticData/Addresses/UI/All", fileName = "AllUIAddresses")]
    public class AllUIAddresses : ScriptableObject
    {
        [field: SerializeField] public CommonUIAddresses CommonUiAddresses { get; private set; } 
        [field: SerializeField] public GameplayUIAddresses GameplayUIAddresses { get; private set; }
    }
}