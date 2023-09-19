using UnityEngine;

namespace CodeBase.Gameplay.Tiles
{
    public class Tile : MonoBehaviour
    {
        public void Construct(Vector2Int coordinates)
        {
            Coordinates = coordinates;
        }

        public Vector2Int Coordinates { get; private set; }
    }
}
