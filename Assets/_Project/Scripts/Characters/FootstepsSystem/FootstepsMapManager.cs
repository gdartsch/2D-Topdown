using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MatchaIsSpent.Characters.FootstepsSystem
{
    /// <summary>
    /// This class is used to get the current floor clip based on the associated tile in the footsteps maps.
    /// It works by getting the tile at the current position and then checking if it is in the list of tiles in the tile data.
    /// If it is, check if it has a higher priority than the current top tile.
    /// If it does, set it as the top tile.
    /// To best results, each possible tile should have a different priority to avoid having the same tile as the top tile when overlapping tilemaps.
    /// </summary>
    public class FootstepsMapManager : MonoBehaviour
    {
        [Tooltip("The tilemaps that contain the footsteps tiles.")]
        [SerializeField] private Tilemap[] footstepsMaps;
        [Tooltip("The list of the tile data.")]
        [SerializeField] private List<SoundTileData> tileDataList;

        /// <summary>
        /// The dictionary that contains the tile data from the tiles associated with the footsteps maps.
        /// </summary>
        private Dictionary<TileBase, SoundTileData> dataFromTiles;

        private void Awake()
        {
            footstepsMaps = FindObjectsOfType<Tilemap>(false);
        }

        private void Start()
        {
            SetTileDataList();
        }

        /// <summary>
        /// Set the tile data list.
        /// </summary>
        private void SetTileDataList()
        {
            dataFromTiles = new Dictionary<TileBase, SoundTileData>();
            foreach (var tileData in tileDataList)
            {
                foreach (var tile in tileData.tiles)
                {
                    dataFromTiles.Add(tile, tileData);
                }
            }
        }

        /// <summary>
        /// Get the current floor clip based on the associated tile in the footsteps maps.
        /// <paramref name="worldPosition"></paramref> is the world position of the player.
        /// </summary>
        /// <param name="worldPosition"></param>
        /// <returns>
        /// The current floor clip based on the associated tile in the footsteps maps.
        /// </returns>
        public AudioClip GetCurrentFloorClip(Vector2 worldPosition)
        {
            List<TileBase> tilesAtPosition = new List<TileBase>();

            foreach (var footstepsMap in footstepsMaps)
            {
                TileBase tile = footstepsMap.GetTile(footstepsMap.WorldToCell(worldPosition));
                if (tile != null)
                {
                    tilesAtPosition.Add(tile);
                }
            }

            TileBase topTile = null;
            int? highestPriority = int.MinValue;

            foreach (var tile in tilesAtPosition)
            {
                dataFromTiles.TryGetValue(tile, out SoundTileData tileData);
                int? tilePriority = tileData?.priority;
                if (tilePriority > highestPriority && tile != null)
                {
                    topTile = tile;
                    highestPriority = tilePriority;
                }
            }

            if (topTile != null)
            {
                return dataFromTiles[topTile].footstepSounds[Random.Range(0, dataFromTiles[topTile].footstepSounds.Length)];
            }

            return null;
        }
    }
}
