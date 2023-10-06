﻿using System;
using CodeBase.Gameplay.Characters.CharacterInfo;
using CodeBase.Infrastructure.Services.Logger;
using UnityEngine;

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
                    return GetDamageWithBonusFromStats(_characterStats.Strength, _characterStats.Level);
                case MainAttribute.Dexterity:
                    return GetDamageWithBonusFromStats(_characterStats.Dexterity, _characterStats.Level);
                case MainAttribute.Intelligence:
                    return GetDamageWithBonusFromStats(_characterStats.Intelligence, _characterStats.Level);
                default:
                    _customLogger.LogError(new Exception($"{_characterStats.MainAttribute}, doesn't exist"));
                    return 0;
            }
        }

        private int GetDamageWithBonusFromStats(int stat, int level) =>
            Mathf.Clamp(CurrentDamage + stat * _totalBonusDamagePerMainStat + level * _totalBonusDamagePerLevel, 0,
                Int32.MaxValue);
    }
}