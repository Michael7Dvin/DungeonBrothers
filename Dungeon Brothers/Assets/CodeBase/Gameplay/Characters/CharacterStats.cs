namespace CodeBase.Gameplay.Characters
{
    public struct CharacterStats
    {
        public int Level { get; private set; }
        public int Intelligence { get; private set; }
        public int Strength { get; private set; }
        public int Dexterity { get; private set; }
        public int Initiative { get; private set; }
        public int TotalStats { get; private set; }

        public CharacterStats(int level,
            int intelligence, 
            int strength, 
            int dexterity, 
            int initiative)
        {
            Level = level;
            Intelligence = intelligence;
            Strength = strength;
            Dexterity = dexterity;
            Initiative = initiative;

            TotalStats = Intelligence + Strength + Dexterity + Initiative;
        }
    }
}