using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.CodeBase.Gameplay.Tiles;

namespace Project.CodeBase.Gameplay.Services.MapGenerator
{
    public interface IMapGenerator
    {
        UniTask<List<Tile>> GenerateMap();
    }
}