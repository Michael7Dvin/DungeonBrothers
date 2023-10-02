using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Services.TurnQueue;
using UnityEngine;

namespace CodeBase.Gameplay.Services.Attack
{
    public class AttackService : IAttackService
    {
        private readonly ITurnQueue _turnQueue;

        public AttackService(ITurnQueue turnQueue)
        {
            _turnQueue = turnQueue;
        }

        public void Attack(ICharacter character)
        {
            ICharacter activeCharacter = _turnQueue.ActiveCharacter.Value;
            
            Debug.Log(activeCharacter.CharacterDamage.GetCharacterDamage());
            
            character.CharacterLogic.Health.TakeDamage(activeCharacter.CharacterDamage.GetCharacterDamage());
            
            Debug.Log(character.CharacterLogic.Health.HealthPoints);
        }
    }
}