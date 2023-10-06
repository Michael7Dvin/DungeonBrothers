using System;

namespace CodeBase.Gameplay.Characters.CharacterInfo
{
    [Serializable]
    public struct MovementStats
    {
        public int MovePoints;
        public bool IsMoveThroughObstacles;
    }
}