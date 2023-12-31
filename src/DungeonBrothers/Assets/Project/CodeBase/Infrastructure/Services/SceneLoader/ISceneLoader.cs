using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Project.CodeBase.Infrastructure.Services.SceneLoader
{
    public interface ISceneLoader
    {
        Scene CurrentScene { get; }
        UniTask Load(SceneType type);
    }
}