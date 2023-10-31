using UnityEngine;

namespace Project.CodeBase.Gameplay.Characters.View.SpriteFlip
{
    public class SpriteFlip : ISpriteFlip
    {
        private readonly Transform _transform;

        public SpriteFlip(Transform transform)
        {
            _transform = transform;
        }

        private bool IsFacingRight => 
            _transform.localScale.x > 0;
        
        public void FlipToCoordinates(Vector2Int characterCoordinates, Vector2Int targetCoordinates)
        {
            int characterX = characterCoordinates.x;
            int targetX = targetCoordinates.x;
            
            if (characterX == targetX)
                return;
            
            if (characterX < targetX && IsFacingRight == false)
                Flip();
            else if (characterX > targetX && IsFacingRight == true)
                Flip();
        }
        
        private void Flip()
        {
            Vector3 scale = _transform.localScale;
            scale.x *= -1;  
            _transform.localScale = scale;
        }
    }
}