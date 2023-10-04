using System;
using CodeBase.Gameplay.Characters.CharacterInfo;

namespace CodeBase.Gameplay.Characters.Logic
{
    public readonly struct CharacterDamage
    {
        private const int _totalBonusDamagePerMainStat = 2;
        private const int _totalBonusDamagePerLevel = 3;
        
        private readonly int _startDamage;
        private readonly CharacterStats _characterStats;
        
        public CharacterDamage(int startDamage, 
            CharacterStats characterStats,
            CharacterAttackType characterAttackType)
        {
            _startDamage = startDamage;
            _characterStats = characterStats;
            CharacterAttackType = characterAttackType;
        }
        
        public CharacterAttackType CharacterAttackType { get; }
        
        public int GetCharacterDamage()
        {
            switch (_characterStats.MainAttribute)
            {
                case MainAttribute.Strength:
                    return _startDamage + (_characterStats.Strength * _totalBonusDamagePerMainStat) 
                                        + (_characterStats.Level * _totalBonusDamagePerLevel);
                case MainAttribute.Dexterity:
                    return _startDamage + (_characterStats.Dexterity * _totalBonusDamagePerMainStat) 
                                        + (_characterStats.Level * _totalBonusDamagePerLevel);;
                case MainAttribute.Intelligence:
                    return _startDamage + (_characterStats.Intelligence * _totalBonusDamagePerMainStat) 
                                        + (_characterStats.Level * _totalBonusDamagePerLevel);;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}