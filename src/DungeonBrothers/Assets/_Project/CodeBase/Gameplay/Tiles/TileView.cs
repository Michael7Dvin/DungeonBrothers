using UnityEngine;

namespace CodeBase.Gameplay.Tiles
{
    public class TileView
    {
        private readonly int _enableOutlineID = Shader.PropertyToID("_EnableOutline");
        private readonly int _outlineColorID = Shader.PropertyToID("_OutlineColor");
        private readonly int _enableHighLightID = Shader.PropertyToID("_EnableHighlight");
        private readonly int _highlightColorID = Shader.PropertyToID("_HighlightColor");

        private readonly Material _tileMaterial;

        public TileView(Material material)
        {
            _tileMaterial = material;
        }

        public void ResetTileView()
        {
            SwitchHighlight(false);
            SwitchOutLine(false);
            ChangeHighlightColor(Color.white);
            ChangeOutLineColor(Color.white);
        }

        public void SwitchOutLine(bool isEnabled) => 
            SetBool(_enableOutlineID, isEnabled);


        public void ChangeOutLineColor(Color color) => 
            SetColor(_outlineColorID, color);

        public void SwitchHighlight(bool isEnabled) => 
            SetBool(_enableHighLightID, isEnabled);

        public void ChangeHighlightColor(Color color) => 
            SetColor(_highlightColorID, color);


        private void SetColor(int propertyID, Color color) => 
            _tileMaterial.SetColor(propertyID, color);
        
        private void SetBool(int propertyID, bool value)
        {
            int intValue = value ? 1 : 0;
            _tileMaterial.SetInt(propertyID, intValue);
        }
    }
}