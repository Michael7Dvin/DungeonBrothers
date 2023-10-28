﻿using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.Providers.CameraProvider
{
    public class CameraProvider : ICameraProvider
    {
        public Camera Camera { get; private set; }

        public void SetCamera(Camera camera) =>
            Camera = camera;
    }
}