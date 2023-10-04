namespace CodeBase.Gameplay.Characters.CharacterInfo
{
    public struct CharacterStats
    {
        public CharacterStats(int level,
            MainAttribute mainAttribute,
            int intelligence, 
            int strength, 
            int dexterity, 
            int initiative)
        {
            Level = level;
            MainAttribute = mainAttribute;
            Intelligence = intelligence;
            Strength = strength;
            Dexterity = dexterity;
            Initiative = initiative;
        }
        
        public int Level { get; }
        public MainAttribute MainAttribute { get; }
        public int Intelligence { get; }
        public int Strength { get; }
        public int Dexterity { get; }
        public int Initiative { get; }
    }
}