using System;
using CodeBase.Gameplay.Characters.CharacterInfo;
using CodeBase.Infrastructure.Services.Logger;

namespace CodeBase.Gameplay.Characters.Logic
{
    public struct CharacterDamage
    {
        private int _totalBonusDamagePerMainStat;
        private int _totalBonusDamagePerLevel;
        
        private CharacterStats _characterStats;

        private ICustomLogger _customLogger;
        
        public int CurrentDamage;
        public CharacterAttackType CharacterAttackType;

        public void Construct(int totalBonusDamagePerMainStat,
            int totalBonusDamagePerLevel,
            CharacterStats characterStats,
            ICustomLogger customLogger)
        {
            _totalBonusDamagePerMainStat = totalBonusDamagePerMainStat;
            _totalBonusDamagePerLevel = totalBonusDamagePerLevel;
            _characterStats = characterStats;
            _customLogger = customLogger;
        }
        
        public int GetCharacterDamage()
        {
            switch (_characterStats.MainAttribute)
            {
                case MainAttribute.Strength:
                    return CurrentDamage +
                           GetDamageFromStats(_characterStats.Strength, _characterStats.Level);
                case MainAttribute.Dexterity:
                    return CurrentDamage +
                           GetDamageFromStats(_characterStats.Dexterity, _characterStats.Level);
                case MainAttribute.Intelligence:
                    return CurrentDamage +
                           GetDamageFromStats(_characterStats.Intelligence, _characterStats.Level);
                default:
                    _customLogger.LogError(new Exception($"{_characterStats.MainAttribute}, doesn't exist"));
                    return 0;
            }
        }

        private int GetDamageFromStats(int stat, int level) =>
            stat * _totalBonusDamagePerMainStat + level * _totalBonusDamagePerLevel;
    }
}