using System;

namespace CodeBase.Gameplay.Characters.CharacterInfo
{
    [Serializable]
    public struct CharacterStats
    {
        public int Initiative;
        
        public int Level;
        
        public MainAttributeID MainAttributeID;
        public int Intelligence;
        public int Strength;
        public int Dexterity;
        
        public int MovePoints;
        public bool IsMoveThroughObstacles;
    }
}