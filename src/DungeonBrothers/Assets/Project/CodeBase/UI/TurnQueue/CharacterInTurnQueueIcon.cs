using _Project.CodeBase.Gameplay.Characters.CharacterInfo;
using UnityEngine;

namespace _Project.CodeBase.UI.TurnQueue
{
    public class CharacterInTurnQueueIcon : MonoBehaviour
    {
        public CharacterID CharacterID { get; private set; }

        public void Construct(CharacterID characterID)
        {
            CharacterID = characterID;
        }

        public void Destroy() => Destroy(gameObject);
    }
}