﻿using UnityEngine;

namespace CodeBase.Gameplay.Tiles
{
    [CreateAssetMenu(menuName = "StaticData/Configs/Tiles/TileColor", fileName = "TileColorConfig")]
    public class TileColorConfig : ScriptableObject
    {
        [field: SerializeField] public Color PathToTile { get; private set; }
        [field: SerializeField] public Color WalkableColorTile { get; private set; }
        [field: SerializeField] public Color EnemyTile { get; private set; }
        [field: SerializeField] public Color AllyTile { get; private set; }
        [field: SerializeField] public Color SelectedTile { get; private set; }
        [field: SerializeField] public Color AttackedTile { get; private set; }
    }
}