using UnityEngine;

namespace CodeBase.Gameplay.Services.Raycast
{
    public class RaycastService : IRaycastService
    {
        public bool TryRaycast<T>(Vector2 origin, Vector2 direction, out T component)
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, direction);

            if (raycastHit2D.collider != null)
            {
                raycastHit2D.collider.TryGetComponent(out component);
                return true;
            }

            component = default(T);
            return false;
        }
    }
}