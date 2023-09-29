namespace CodeBase.Gameplay.Characters
{
    public struct CharacterStats
    {
        public int Level { get; }
        public int Intelligence { get; }
        public int Strength { get; }
        public int Dexterity { get; }
        public int Initiative { get; }
        public int MovePoints { get; }
        public bool IsMoveThroughObstacles { get; }
        public int TotalStats => Intelligence + Strength + Dexterity + Initiative;

        public CharacterStats(int level,
            int intelligence, 
            int strength, 
            int dexterity, 
            int initiative,
            int movePoints,
            bool isMoveThroughObstacles)
        {
            Level = level;
            Intelligence = intelligence;
            Strength = strength;
            Dexterity = dexterity;
            Initiative = initiative;
            MovePoints = movePoints;
            IsMoveThroughObstacles = isMoveThroughObstacles;
        }
    }
}