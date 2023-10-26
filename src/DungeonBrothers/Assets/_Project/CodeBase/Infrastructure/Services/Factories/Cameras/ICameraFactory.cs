using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services.Factories.Cameras
{
    public interface ICameraFactory
    {
        public UniTask WarmUp();

        public UniTask<UnityEngine.Camera> Create();
    }
}