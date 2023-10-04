using System;
using CodeBase.Gameplay.Characters.CharacterInfo;
using CodeBase.Infrastructure.Services.Logger;

namespace CodeBase.Gameplay.Characters.Logic
{
    public readonly struct CharacterDamage
    {
        private readonly int _totalBonusDamagePerMainStat;
        private readonly int _totalBonusDamagePerLevel;
        
        private readonly int _currentDamage;
        private readonly CharacterStats _characterStats;

        private readonly ICustomLogger _customLogger;
        
        public CharacterDamage(int totalBonusDamagePerMainStat,
            int totalBonusDamagePerLevel,
            int startDamage, 
            CharacterStats characterStats,
            CharacterAttackType characterAttackType,
            ICustomLogger customLogger)
        {
            _totalBonusDamagePerMainStat = totalBonusDamagePerMainStat;
            _totalBonusDamagePerLevel = totalBonusDamagePerLevel;
            
            _currentDamage = startDamage;
            
            _characterStats = characterStats;
            CharacterAttackType = characterAttackType;
            _customLogger = customLogger;
        }
        
        public CharacterAttackType CharacterAttackType { get; }
        
        public int GetCharacterDamage()
        {
            switch (_characterStats.MainAttribute)
            {
                case MainAttribute.Strength:
                    return _currentDamage +
                           GetDamageFromStats(_characterStats.Strength, _characterStats.Level);
                case MainAttribute.Dexterity:
                    return _currentDamage +
                           GetDamageFromStats(_characterStats.Dexterity, _characterStats.Level);
                case MainAttribute.Intelligence:
                    return _currentDamage +
                           GetDamageFromStats(_characterStats.Initiative, _characterStats.Level);
                default:
                    _customLogger.LogError(new Exception($"{_characterStats.MainAttribute}, doesn't exist"));
                    return 0;
            }
        }

        private int GetDamageFromStats(int stat, int level) =>
            stat * _totalBonusDamagePerMainStat + level * _totalBonusDamagePerLevel;
    }
}