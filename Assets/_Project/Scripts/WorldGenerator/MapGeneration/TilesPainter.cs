using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MatchaIsSpent.WorldGeneration
{
    /// <summary>
    /// This class is responsible for painting the tiles on the map.
    /// </summary>
    public class TilesPainter : MonoBehaviour
    {
        [field: Header("References")]
        [Tooltip("The world generation settings")]
        [field: SerializeField] public WorldGenerationSettings WorldGenerationSettings { get; private set; } = null;
        [Tooltip("The room generator")]
        [field: SerializeField] public RoomGenerator RoomData { get; private set; } = null;

        /// <summary>
        /// Paints the tiles on the map.
        /// <paramref name="position"/> The position of the tile to be painted.
        /// <paramref name="binaryType"/> The type of the tile to be painted.
        /// <paramref name="map"/> The tilemap to be painted.
        /// <paramref name="isSecondIteration"/> If the painting is done on the second iteration.
        /// <paramref name="isIsland"/> If the painting is done on the island.
        /// <paramref name="cliffsUnderIslandTilemap"/> The tilemap of the cliffs under the island.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="binaryType"></param>
        /// <param name="map"></param>
        /// <param name="isSecondIteration"></param>
        /// <param name="isIsland"></param>
        /// <param name="cliffsUnderIslandTilemap"></param>
        public void PaintSingleCornerWall(Vector2Int position, string binaryType, Tilemap map, bool isSecondIteration = false,
                                               bool isIsland = false, Tilemap cliffsUnderIslandTilemap = null)
        {
            int typeAsInt = Convert.ToInt32(binaryType, 2);
            TileBase tile = null;

            if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
            {
                tile = isIsland ? RoomData.IslandRoomTiles[1].Tiles[WorldGenerationSettings.randomTileIndex] :
                       isSecondIteration ? RoomData.SmallGrassRoomTiles[1].Tiles[WorldGenerationSettings.randomTileIndex] :
                       RoomData.BigGrassRoomTiles[1].Tiles[WorldGenerationSettings.randomTileIndex];

                cliffsUnderIslandTilemap?.SetTile(new Vector3Int(position.x, position.y - 1, 0), RoomData.MainRoomTiles[2].Tiles[0]);
            }
            else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
            {
                tile = isIsland ? RoomData.IslandRoomTiles[2].Tiles[WorldGenerationSettings.randomTileIndex] :
                       isSecondIteration ? RoomData.SmallGrassRoomTiles[2].Tiles[WorldGenerationSettings.randomTileIndex] :
                       RoomData.BigGrassRoomTiles[2].Tiles[WorldGenerationSettings.randomTileIndex];

                cliffsUnderIslandTilemap?.SetTile(new Vector3Int(position.x, position.y - 1, 0), RoomData.MainRoomTiles[2].Tiles[0]);
            }
            else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
            {
                tile = isIsland ? RoomData.IslandRoomTiles[3].Tiles[WorldGenerationSettings.randomTileIndex] :
                       isSecondIteration ? RoomData.SmallGrassRoomTiles[3].Tiles[WorldGenerationSettings.randomTileIndex] :
                       RoomData.BigGrassRoomTiles[3].Tiles[WorldGenerationSettings.randomTileIndex];

                cliffsUnderIslandTilemap?.SetTile(new Vector3Int(position.x, position.y - 1, 0), RoomData.MainRoomTiles[2].Tiles[0]);
            }
            else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
            {
                tile = isIsland ? RoomData.IslandRoomTiles[4].Tiles[WorldGenerationSettings.randomTileIndex] :
                       isSecondIteration ? RoomData.SmallGrassRoomTiles[4].Tiles[WorldGenerationSettings.randomTileIndex] :
                      RoomData.BigGrassRoomTiles[4].Tiles[WorldGenerationSettings.randomTileIndex];

                cliffsUnderIslandTilemap?.SetTile(new Vector3Int(position.x, position.y - 1, 0), RoomData.MainRoomTiles[2].Tiles[0]);
            }
            else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
            {
                tile = isIsland ? RoomData.IslandRoomTiles[5].Tiles[WorldGenerationSettings.randomTileIndex] :
                       isSecondIteration ? RoomData.SmallGrassRoomTiles[5].Tiles[WorldGenerationSettings.randomTileIndex] :
                       RoomData.BigGrassRoomTiles[5].Tiles[WorldGenerationSettings.randomTileIndex];
            }
            else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
            {
                tile = isIsland ? RoomData.IslandRoomTiles[6].Tiles[WorldGenerationSettings.randomTileIndex] :
                       isSecondIteration ? RoomData.SmallGrassRoomTiles[6].Tiles[WorldGenerationSettings.randomTileIndex] :
                       RoomData.BigGrassRoomTiles[6].Tiles[WorldGenerationSettings.randomTileIndex];
            }
            else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
            {
                tile = isIsland ? RoomData.IslandRoomTiles[7].Tiles[WorldGenerationSettings.randomTileIndex] :
                       isSecondIteration ? RoomData.SmallGrassRoomTiles[7].Tiles[WorldGenerationSettings.randomTileIndex] :
                       RoomData.BigGrassRoomTiles[7].Tiles[WorldGenerationSettings.randomTileIndex];
            }
            else if (WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt))
            {
                tile = isIsland ? RoomData.IslandRoomTiles[8].Tiles[WorldGenerationSettings.randomTileIndex] :
                       isSecondIteration ? RoomData.SmallGrassRoomTiles[8].Tiles[WorldGenerationSettings.randomTileIndex] :
                       RoomData.BigGrassRoomTiles[8].Tiles[WorldGenerationSettings.randomTileIndex];

                cliffsUnderIslandTilemap?.SetTile(new Vector3Int(position.x, position.y - 1, 0), RoomData.MainRoomTiles[2].Tiles[0]);
            }

            if (tile != null)
                PaintSingleTile(map, tile, position);
        }

        /// <summary>
        /// Paints the tiles on the map.
        /// <paramref name="position"/> The position of the tile to be painted.
        /// <paramref name="binaryType"/> The type of the tile to be painted.
        /// <paramref name="map"/> The tilemap to be painted.
        /// <paramref name="isSecondIteration"/> If the painting is done on the second iteration.
        /// <paramref name="isIsland"/> If the painting is done on the island.
        /// <paramref name="cliffsUnderIslandTilemap"/> The tilemap of the cliffs under the island.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="binaryType"></param>
        /// <param name="map"></param>
        /// <param name="isSecondIteration"></param>
        /// <param name="isIsland"></param>
        /// <param name="cliffsUnderIslandTilemap"></param>
        public void PaintSingleBasicWall(Vector2Int position, string binaryType, Tilemap submapGrassTilemap, bool isSecondIteration = false, bool isIsland = false,
                                         Tilemap cliffsUnderIslandTilemap = null)
        {
            int typeAsInt = Convert.ToInt32(binaryType, 2);
            TileBase tile = null;

            if (WallTypesHelper.wallTop.Contains(typeAsInt))
            {
                tile = isIsland ? RoomData.IslandRoomTiles[9].Tiles[WorldGenerationSettings.randomTileIndex] :
                    isSecondIteration ? RoomData.SmallGrassRoomTiles[9].Tiles[WorldGenerationSettings.randomTileIndex] :
                        RoomData.BigGrassRoomTiles[9].Tiles[WorldGenerationSettings.randomTileIndex];
            }
            else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
            {
                tile = isIsland ? RoomData.IslandRoomTiles[10].Tiles[WorldGenerationSettings.randomTileIndex] :
                    isSecondIteration ? RoomData.SmallGrassRoomTiles[10].Tiles[WorldGenerationSettings.randomTileIndex] :
                        RoomData.BigGrassRoomTiles[10].Tiles[WorldGenerationSettings.randomTileIndex];
            }
            else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
            {
                tile = isIsland ? RoomData.IslandRoomTiles[11].Tiles[WorldGenerationSettings.randomTileIndex] :
                    isSecondIteration ? RoomData.SmallGrassRoomTiles[11].Tiles[WorldGenerationSettings.randomTileIndex] :
                        RoomData.BigGrassRoomTiles[11].Tiles[WorldGenerationSettings.randomTileIndex];
            }
            else if (WallTypesHelper.wallBottom.Contains(typeAsInt))
            {
                tile = isIsland ? RoomData.IslandRoomTiles[8].Tiles[WorldGenerationSettings.randomTileIndex] :
                    isSecondIteration ? RoomData.SmallGrassRoomTiles[8].Tiles[WorldGenerationSettings.randomTileIndex] :
                        RoomData.BigGrassRoomTiles[8].Tiles[WorldGenerationSettings.randomTileIndex];

                cliffsUnderIslandTilemap?.SetTile(new Vector3Int(position.x, position.y - 1, 0), RoomData.MainRoomTiles[2].Tiles[0]);
            }
            else if (WallTypesHelper.wallFull.Contains(typeAsInt))
            {
                tile = isIsland ? RoomData.IslandRoomTiles[7].Tiles[WorldGenerationSettings.randomTileIndex] :
                    isSecondIteration ? RoomData.SmallGrassRoomTiles[7].Tiles[WorldGenerationSettings.randomTileIndex] :
                        RoomData.BigGrassRoomTiles[7].Tiles[WorldGenerationSettings.randomTileIndex];
            }

            if (tile != null)
                PaintSingleTile(submapGrassTilemap, tile, position);
        }

        /// <summary>
        /// Paints the tiles on the map.
        /// <paramref name="tileMap"/> The tilemap to be painted.
        /// <paramref name="tile"/> The type of the tile to be painted.
        /// <paramref name="position"/> The position of the tile to be painted.
        /// </summary>
        /// <param name="tileMap"></param>
        /// <param name="tile"></param>
        /// <param name="position"></param>
        private void PaintSingleTile(Tilemap tileMap, TileBase tile, Vector2Int position)
        {
            Vector3Int tilePosition = tileMap.WorldToCell((Vector3Int)position);
            tileMap.SetTile(tilePosition, tile);
        }
    }
}
