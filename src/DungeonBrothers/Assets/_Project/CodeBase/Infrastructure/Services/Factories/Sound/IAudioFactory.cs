using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Infrastructure.Services.Factories.Sound
{
    public interface IAudioFactory
    {
        public UniTask WarmUp();
        public UniTask Create();
    }
}