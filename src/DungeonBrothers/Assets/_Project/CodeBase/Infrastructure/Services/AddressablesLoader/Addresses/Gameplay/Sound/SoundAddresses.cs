﻿using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.CodeBase.Infrastructure.Services.AddressablesLoader.Addresses.Gameplay.Sound
{
    [CreateAssetMenu(menuName = "StaticData/Addresses/Gameplay/Sounds", fileName = "SoundsAddresses")]
    public class SoundAddresses : ScriptableObject
    {
        public AssetReferenceGameObject SoundSource;

        public AssetReference ss;
    }
}