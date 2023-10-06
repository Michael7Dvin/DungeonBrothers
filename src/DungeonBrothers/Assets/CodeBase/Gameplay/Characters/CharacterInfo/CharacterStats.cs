using System;

namespace CodeBase.Gameplay.Characters.CharacterInfo
{
    [Serializable]
    public struct CharacterStats
    {
        public int Level;
        public MainAttribute MainAttribute;
        public int Intelligence;
        public int Strength;
        public int Dexterity;
        public int Initiative;
    }
}