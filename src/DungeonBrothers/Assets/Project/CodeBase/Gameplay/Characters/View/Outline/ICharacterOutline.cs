using UnityEngine;

namespace Project.CodeBase.Gameplay.Characters.View.Outline
{
    public interface ICharacterOutline
    {
        void SwitchOutLine(bool isEnabled);
        void ChangeColor(Color color);
    }
}