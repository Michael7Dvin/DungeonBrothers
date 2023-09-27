using CodeBase.Gameplay.Tiles;
using CodeBase.Infrastructure.Services.AddressablesLoader.Loader;
using CodeBase.Infrastructure.Services.StaticDataProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Services.Factories.TileFactory
{
    public class TileFactory : ITileFactory
    {
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly IObjectResolver _objectResolver;
        private readonly AssetReferenceGameObject _tileReference;

        public TileFactory(IAddressablesLoader addressablesLoader,
            IObjectResolver objectResolver,
            IStaticDataProvider staticDataProvider)
        {
            _addressablesLoader = addressablesLoader;
            _objectResolver = objectResolver;
            _tileReference = staticDataProvider.AssetsAddresses.Tile;
        }

        public async UniTask WarmUp() => 
            await _addressablesLoader.LoadComponent<Tile>(_tileReference);

        public async UniTask<Tile> Create(Vector3 position, Vector2Int coordinates, Transform parent)
        {
            Tile prefab = await _addressablesLoader.LoadComponent<Tile>(_tileReference);
            Tile tile = _objectResolver.Instantiate(prefab, position, Quaternion.identity, parent);

            TileView tileView = CreateTileView(tile);
            TileLogic tileLogic = CreateTileLogic(coordinates);
            tile.Construct(tileLogic, tileView);
            
            return tile;
        }

        private TileLogic CreateTileLogic(Vector2Int coordinate)
        {
            TileLogic tileLogic = new TileLogic(false, true, coordinate);
            _objectResolver.Inject(tileLogic);
            return tileLogic;
        }

        private TileView CreateTileView(Tile tile)
        {
            SpriteRenderer spriteRenderer = tile.GetComponent<SpriteRenderer>();
            Material material = spriteRenderer.material;
            return new TileView(material);
        }
    }
}