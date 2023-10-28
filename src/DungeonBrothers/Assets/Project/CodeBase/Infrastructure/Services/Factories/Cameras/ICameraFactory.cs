using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.Services.Factories.Cameras
{
    public interface ICameraFactory
    {
        public UniTask WarmUp();

        public UniTask<UnityEngine.Camera> Create();
    }
}