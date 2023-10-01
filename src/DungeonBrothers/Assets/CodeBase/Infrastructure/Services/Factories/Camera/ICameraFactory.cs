using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services.Factories.Camera
{
    public interface ICameraFactory
    {
        public UniTask WarmUp();

        public UniTask<UnityEngine.Camera> Create();
    }
}