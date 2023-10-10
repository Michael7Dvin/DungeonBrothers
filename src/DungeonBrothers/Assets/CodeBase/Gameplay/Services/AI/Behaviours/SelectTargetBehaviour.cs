using CodeBase.Gameplay.Characters;
using CodeBase.Gameplay.Characters.CharacterInfo;
using CodeBase.Gameplay.PathFinder;
using CodeBase.Gameplay.Services.TurnQueue;
using UnityEngine;
using VContainer;

namespace CodeBase.Gameplay.Services.AI.Behaviours
{
    public class SelectTargetBehaviour : ISelectTargetBehaviour
    {
        private readonly ITurnQueue _turnQueue;
        
        private ICharacter _currentTarget;
        
        public SelectTargetBehaviour(ITurnQueue turnQueue)
        {
            _turnQueue = turnQueue;
        }
        
        public ICharacter GetTarget()
        {
            _currentTarget = null;
            
            foreach (var character in _turnQueue.Characters)
            {
                if (character.CharacterTeam == CharacterTeam.Ally)
                {
                    if (_currentTarget == null)
                    {
                        _currentTarget = character;
                        continue;
                    }

                    if (GetDistanceToTarget(_currentTarget) > GetDistanceToTarget(character))
                        _currentTarget = character;
                }
            }

            return _currentTarget;
        }

        private float GetDistanceToTarget(ICharacter target)
        {
            ICharacter activeCharacter = _turnQueue.ActiveCharacter.Value;

            return Vector2.Distance(activeCharacter.Coordinate, target.Coordinate);
        }
    }
}