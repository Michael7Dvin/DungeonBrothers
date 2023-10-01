namespace CodeBase.Gameplay.Characters.CharacterInfo
{
    public struct MovementStats
    {
        public MovementStats(int movePoints, bool isMoveThroughObstacles)
        {
            MovePoints = movePoints;
            IsMoveThroughObstacles = isMoveThroughObstacles;
        }

        public int MovePoints { get; }
        public bool IsMoveThroughObstacles { get; }
    }
}