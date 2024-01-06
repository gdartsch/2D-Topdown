using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MatchaIsSpent.WorldGeneration
{
    /// <summary>
    /// This class is used to store all the tiles information in the game.
    /// </summary>
    public class TilesInformationManager : MonoBehaviour
    {
        [field: Header("Tiles Information")]
        [Tooltip("The list of the tilemaps in the game")]
        [field: SerializeField] public List<Tilemap> Tilemaps { get; set; } = new List<Tilemap>();
        [Tooltip("The list of the tile data in the game")]
        [field: SerializeField] public TileData[] TileDatas { get; private set; }
        /// <summary>
        /// The dictionary of the tiles data in the game.
        /// </summary>
        public Dictionary<TileBase, TileData> TilesData { get; private set; }
        /// <summary>
        /// The instance of the class.
        /// </summary>
        public static TilesInformationManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                TilesData = new Dictionary<TileBase, TileData>();
            }
            else
                Destroy(gameObject);
            TilesData = new Dictionary<TileBase, TileData>();

            for (short i = 0; i < TileDatas.Length; i++)
            {
                for (short j = 0; j < TileDatas[i].Tiles.Length; j++)
                {
                    TilesData.Add(TileDatas[i].Tiles[j], TileDatas[i]);
                }
            }
        }

        private void Start()
        {
            //Just to avoid hard to find bugs that I had no time to address
            if (Tilemaps.Find(x => x == null))
            {
                Tilemaps = FindObjectsOfType<Tilemap>().ToList<Tilemap>();
            }
        }

        /// <summary>
        /// Returns the tile data of the tile at the given position.
        /// <paramref name="worldPosition"/> The position of the tile.
        /// </summary>
        /// <param name="worldPosition"></param>
        /// <returns>
        /// A list of the tile data of the tile at the given position.
        /// </returns>
        public List<TileType> GetTileData(Vector2 worldPosition)
        {
            List<TileType> tileTypes = new List<TileType>();

            foreach (Tilemap tilemap in Tilemaps)
            {
                TileBase tile = tilemap.GetTile(tilemap.WorldToCell(worldPosition));
                if (tile != null)
                    tileTypes.Add(TilesData[tile].TileType);
            }

            if (tileTypes.Count == 0)
            {
                return null;
            }

            return tileTypes;
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}
