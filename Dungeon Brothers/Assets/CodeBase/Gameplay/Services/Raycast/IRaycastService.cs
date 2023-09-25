using UnityEngine;

namespace CodeBase.Gameplay.Services.Raycast
{
    public interface IRaycastService
    {
        public bool TryRaycast<T>(Vector2 origin, Vector2 direction, out T component);
    }
}