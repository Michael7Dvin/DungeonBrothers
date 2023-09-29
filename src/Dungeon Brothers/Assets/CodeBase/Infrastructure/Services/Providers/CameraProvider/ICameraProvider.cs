using UnityEngine;

namespace CodeBase.Infrastructure.Services.Providers.CameraProvider
{
    public interface ICameraProvider
    {
        public Camera Camera { get; }

        public void SetCamera(Camera camera);
    }
}