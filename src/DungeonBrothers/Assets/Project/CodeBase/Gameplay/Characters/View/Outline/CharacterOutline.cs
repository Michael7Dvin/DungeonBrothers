using UnityEngine;

namespace Project.CodeBase.Gameplay.Characters.View.Outline
{
    public class CharacterOutline
    {
        private readonly int _enableOutlineID = Shader.PropertyToID("_EnableOutline");
        private readonly int _outlineColorID = Shader.PropertyToID("_OutlineColor");

        private readonly Material _material;

        public CharacterOutline(Material material)
        {
            _material = material;
        }
        
        public void SwitchOutLine(bool isEnabled)
        {
            int intValue = isEnabled ? 1 : 0;
            _material.SetInt(_enableOutlineID, intValue);
        }

        public void ChangeColor(Color color) => 
            _material.SetColor(_outlineColorID, color);
    }
}