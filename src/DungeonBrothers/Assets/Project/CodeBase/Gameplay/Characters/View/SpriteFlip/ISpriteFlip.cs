using UnityEngine;

namespace Project.CodeBase.Gameplay.Characters.View.SpriteFlip
{
    public interface ISpriteFlip
    {
        void FlipToCoordinates(Vector2Int characterCoordinates, Vector2Int targetCoordinates);
    }
}