using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Services.SceneLoading
{
    public interface ISceneLoader
    {
        Scene CurrentScene { get; }
        UniTask Load(SceneType type);
    }
}