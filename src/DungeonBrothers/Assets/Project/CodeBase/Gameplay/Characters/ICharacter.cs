using Project.CodeBase.Gameplay.Characters.CharacterInfo;
using Project.CodeBase.Gameplay.Characters.Logic;
using Project.CodeBase.Gameplay.Characters.View;
using UnityEngine;

namespace Project.CodeBase.Gameplay.Characters
{
    public interface ICharacter
    {
        public CharacterID ID { get; }
        public CharacterTeam Team { get; }
        public CharacterStats Stats { get; }
        public CharacterDamage Damage { get; }
        public ICharacterLogic Logic { get; }
        public ICharacterView View { get; }
        public bool IsInBattle { get; set; }
        public GameObject GameObject { get; }
    }
}