namespace CodeBase.Gameplay.Characters.CharacterInfo
{
    public struct CharacterStats
    {
        public int Level { get; }
        public int Intelligence { get; }
        public int Strength { get; }
        public int Dexterity { get; }
        public int Initiative { get; }

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
        }
    }
}