using UnityEngine;
using UnityEngine.Tilemaps;

namespace MatchaIsSpent.Data
{
    /// <summary>
    /// A class that holds a name and a list of tiles.
    /// </summary>
    [System.Serializable]
    public class TileObject
    {
        [Tooltip("The name of the tile object.")]
        [field: SerializeField] public string Name { get; private set; } = null;
        [Tooltip("The tiles that make up the tile object.")]
        [field: SerializeField] public TileBase[] Tiles { get; private set; } = null;
    }
}
