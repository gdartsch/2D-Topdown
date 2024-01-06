using System.Collections.Generic;
using UnityEngine;

namespace MatchaIsSpent.WorldGeneration
{
    /// <summary>
    /// This class is used to store all the settings for the world generation.
    /// </summary>
    public class WorldGenerationSettings : MonoBehaviour
    {
        [field: Header("Map Settings")]
        [Tooltip("The size of the map in tiles")]
        [field: SerializeField] public Vector2Int RoomSize { get; private set; } = new Vector2Int(10, 10);
        [Tooltip("The number of submaps to be generated")]
        [field: SerializeField] public int Submaps { get; private set; } = 3;
        [Tooltip("The offset between the submaps")]
        [field: SerializeField] public int Offset { get; private set; } = 1;
        [Tooltip("The minimum widht of a room")]
        [field: SerializeField] public int MinRoomWidth { get; private set; } = 10;
        [Tooltip("The minimum height of a room")]
        [field: SerializeField] public int MinRoomHeight { get; private set; } = 10;

        [field: Space(1), Header("Random Walk Settings")]
        [Tooltip("The settings for the random walk algorithm used to generate the big grass path")]
        [field: SerializeField] public SimpleRandomWalkSO BigGrassRandomWalkSettings { get; private set; } = null;
        [Tooltip("The settings for the random walk algorithm used to generate the small grass path")]
        [field: SerializeField] public SimpleRandomWalkSO SmallGrassRandomWalkSettings { get; private set; } = null;
        [Tooltip("The settings for the random walk algorithm used to generate the island path")]
        [field: SerializeField] public SimpleRandomWalkSO SmallIslandRandomWalkSettings { get; private set; } = null;

        [field: Space(1), Header("References")]
        [Tooltip("The tiles data manager")]
        [field: SerializeField] public TilesInformationManager TilesDataManager = null;
        [Tooltip("The world map container")]
        [field: SerializeField] public Transform WorldMap { get; private set; } = null;
        [Tooltip("The grass map container")]
        [field: SerializeField] public Transform GrassMap { get; private set; } = null;
        [Tooltip("The grass over map container")]
        [field: SerializeField] public GameObject GrassOverMap { get; private set; } = null;
        [Tooltip("The map data")]
        [field: SerializeField] public MapData MapData { get; private set; } = null;
        [Tooltip("The room generator")]
        [field: SerializeField] public RoomGenerator RoomGenerator { get; private set; }

        /// <summary>
        /// The index of the tile to be used for the random tile.
        /// </summary>
        [HideInInspector] public int randomTileIndex = 0;

        /// <summary>
        /// The list of the neighbour directions.
        /// </summary>
        public static List<Vector2Int> neighbourFourDirections = new List<Vector2Int>
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };
    }
}
