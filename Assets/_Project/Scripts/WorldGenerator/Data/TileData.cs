using UnityEngine;
using UnityEngine.Tilemaps;

namespace MatchaIsSpent.WorldGeneration
{
    /// <summary>
    /// This enum contains all possible tile types.
    /// </summary>
    public enum TileType
    {
        Floor,
        Cliff,
        CliffEdge,
        Grass,
        Wall
    }

    /// <summary>
    /// This class contains data for a tile.
    /// </summary>
    [CreateAssetMenu(fileName = "TileData", menuName = "MatchaIsSpent/WorldGeneration/TileData", order = 1)]
    public class TileData : ScriptableObject
    {
        [field: SerializeField] public TileBase[] Tiles { get; private set; }
        [field: SerializeField] public TileType TileType { get; private set; }
    }
}
