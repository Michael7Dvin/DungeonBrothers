using UnityEngine;

namespace Project.CodeBase.Infrastructure.Services.StaticDataProvider
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Dungeon", fileName = "DungeonConfig")]
    public class DungeonConfig : ScriptableObject
    {
        public int MaxRoomsInDungeon;
        public int MaxRoomsInBranch;
    }
}