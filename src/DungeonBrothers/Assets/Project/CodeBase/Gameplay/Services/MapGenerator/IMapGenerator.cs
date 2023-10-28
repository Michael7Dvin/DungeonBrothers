using System.Collections.Generic;
using _Project.CodeBase.Gameplay.Tiles;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Gameplay.Services.MapGenerator
{
    public interface IMapGenerator
    {
        UniTask<List<Tile>> GenerateMap();
    }
}