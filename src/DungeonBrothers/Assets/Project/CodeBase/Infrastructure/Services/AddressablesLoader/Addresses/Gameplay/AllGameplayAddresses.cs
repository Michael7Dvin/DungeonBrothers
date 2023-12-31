﻿using Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.Gameplay.Sound;
using Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.Gameplay.Tiles;
using UnityEngine;

namespace Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.Gameplay
{
    [CreateAssetMenu(menuName = "StaticData/Addresses/Gameplay/AllGameplayAddresses", fileName = "AllGameplayAddresses")]
    public class AllGameplayAddresses : ScriptableObject
    {
        public TilesAddresses TilesAddresses;
        public SoundAddresses SoundAddresses;
    }
}