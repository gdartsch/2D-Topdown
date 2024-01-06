using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MatchaIsSpent.WorldGeneration
{
    /// <summary>
    /// Class that generates the edges of the map
    /// </summary>
    public class EdgeGenerator : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Reference to the world generation settings")]
        [SerializeField] private WorldGenerationSettings worldGenerationSettings = null;
        [Tooltip("Reference to the room data")]
        [SerializeField] private RoomGenerator roomData = null;
        [Tooltip("Reference to the tiles painter")]
        [SerializeField] private TilesPainter painter = null;

        /// <summary>
        /// Generates the limits of the map
        /// <paramref name="colMap"/> The tilemap that contains the colliders
        /// <paramref name="cliffsMap"/> The tilemap that contains the cliffs
        /// <paramref name="room"/> The room that contains the edges
        /// <paramref name="lastIteration"/> If this is the last iteration of the map generation
        /// <paramref name="firstIteration"/> If this is the first iteration of the map generation
        /// </summary>
        /// <param name="colMap"></param>
        /// <param name="cliffsMap"></param>
        /// <param name="room"></param>
        /// <param name="lastIteration"></param>
        /// <param name="firstIteration"></param>
        public void GenerateMapCollider(Tilemap colMap, Tilemap cliffsMap, Room room, bool lastIteration, bool firstIteration = false)
        {
            HashSet<Vector2Int> mapTiles = new HashSet<Vector2Int>();

            HashSet<Vector2Int> colliderTiles = new HashSet<Vector2Int>();

            mapTiles.UnionWith(room.FloorTiles);

            foreach (Vector2Int tilePosition in room.FloorTiles)
            {
                foreach (Vector2Int direction in WorldGenerationSettings.neighbourFourDirections)
                {
                    Vector2Int newPosition = new Vector2Int(tilePosition.x, tilePosition.y) + direction;

                    if (!mapTiles.Contains(newPosition))
                    {
                        bool shouldBeCliff = !lastIteration && direction == Vector2Int.down;
                        colliderTiles.Add(newPosition);

                        if (shouldBeCliff)
                            cliffsMap.SetTile(new Vector3Int(newPosition.x, newPosition.y, 0), roomData.MainRoomTiles[1].Tiles[worldGenerationSettings.randomTileIndex]);

                        if (!firstIteration && direction == Vector2Int.up)
                            continue;

                        colMap.SetTile((Vector3Int)newPosition, shouldBeCliff ? roomData.MainRoomTiles[2].Tiles[0] :
                                       roomData.MainRoomTiles[3].Tiles[0]);
                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// Generates the edges of the grass and islands
        /// <paramref name="floorPositions"/> The floor positions of the map
        /// <paramref name="submapGrassTilemap"/> The tilemap that contains the grass
        /// <paramref name="isSeconIteration"/> If this is the second iteration of the map generation
        /// <paramref name="isIsland"/> If this is an island
        /// <paramref name="cliffsUnderIslandTilemap"/> The tilemap that contains the cliffs under the island
        /// </summary>
        /// <param name="floorPositions"></param>
        /// <param name="submapGrassTilemap"></param>
        /// <param name="isSeconIteration"></param>
        /// <param name="isIsland"></param>
        /// <param name="cliffsUnderIslandTilemap"></param>
        public void CreateBorders(HashSet<Vector2Int> floorPositions, Tilemap submapGrassTilemap,
                                  bool isSeconIteration = false, bool isIsland = false, Tilemap cliffsUnderIslandTilemap = null)
        {
            HashSet<Vector2Int> basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.CardinalDirectionList);

            HashSet<Vector2Int> cornerWallPositions = FindWallsInDirections(floorPositions, Direction2D.DiagonalDirectionList);

            CreateBasicWall(basicWallPositions, floorPositions, submapGrassTilemap, isSeconIteration, isIsland, cliffsUnderIslandTilemap ? cliffsUnderIslandTilemap : null);

            CreateCornerWall(cornerWallPositions, floorPositions, submapGrassTilemap, isSeconIteration, isIsland, cliffsUnderIslandTilemap ? cliffsUnderIslandTilemap : null);
        }

        /// <summary>
        /// Creates the corner walls
        /// <paramref name="cornerWallPositions"/> The corner wall positions
        /// <paramref name="floorPositions"/> The floor positions
        /// <paramref name="submapGrassTilemap"/> The tilemap that contains the grass
        /// <paramref name="isSecondIteration"/> If this is the second iteration of the map generation
        /// <paramref name="isIsland"/> If this is an island
        /// <paramref name="cliffsUnderIslandTilemap"/> The tilemap that contains the cliffs under the island
        /// </summary>
        /// <param name="cornerWallPositions"></param>
        /// <param name="floorPositions"></param>
        /// <param name="submapGrassTilemap"></param>
        /// <param name="isSecondIteration"></param>
        /// <param name="isIsland"></param>
        /// <param name="cliffsUnderIslandTilemap"></param>
        private void CreateCornerWall(HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions, Tilemap submapGrassTilemap, bool isSecondIteration = false,
                                      bool isIsland = false, Tilemap cliffsUnderIslandTilemap = null)
        {
            foreach (Vector2Int position in cornerWallPositions)
            {
                string neighbourBinaryType = "";

                foreach (Vector2Int direction in Direction2D.EightDirectionList)
                {
                    Vector2Int neighbourPosition = position + direction;
                    if (floorPositions.Contains(neighbourPosition))
                        neighbourBinaryType += "1";
                    else
                        neighbourBinaryType += "0";
                }

                painter.PaintSingleCornerWall(position, neighbourBinaryType, submapGrassTilemap, isSecondIteration,
                                      isIsland, cliffsUnderIslandTilemap ? cliffsUnderIslandTilemap : null);
            }
        }

        /// <summary>
        /// Finds the walls in the directions around the floor positions
        /// <paramref name="floorPositions"/> The floor positions
        /// <paramref name="directionsList"/> The directions list
        /// </summary>
        /// <param name="floorPositions"></param>
        /// <param name="directionsList"></param>
        /// <returns>
        /// The walls in the directions around the floor positions
        /// </returns>
        private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionsList)
        {
            HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();

            foreach (Vector2Int floorPosition in floorPositions)
            {
                foreach (Vector2Int direction in directionsList)
                {
                    Vector2Int neighbourPosition = floorPosition + direction;

                    if (!floorPositions.Contains(neighbourPosition))
                        wallPositions.Add(neighbourPosition);
                }
            }

            return wallPositions;
        }

        /// <summary>
        /// Creates the basic walls
        /// <paramref name="basicWallPositions"/> The basic wall positions
        /// <paramref name="floorPositions"/> The floor positions
        /// <paramref name="submapGrassTilemap"/> The tilemap that contains the grass
        /// <paramref name="isSecondIteration"/> If this is the second iteration of the map generation
        /// <paramref name="isIsland"/> If this is an island
        /// <paramref name="cliffsUnderIslandTilemap"/> The tilemap that contains the cliffs under the island
        /// </summary>
        /// <param name="basicWallPositions"></param>
        /// <param name="floorPositions"></param>
        /// <param name="submapGrassTilemap"></param>
        /// <param name="isSecondIteration"></param>
        /// <param name="isIsland"></param>
        /// <param name="cliffsUnderIslandTilemap"></param>
        private void CreateBasicWall(HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions, Tilemap submapGrassTilemap,
                                     bool isSecondIteration = false, bool isIsland = false, Tilemap cliffsUnderIslandTilemap = null)
        {
            foreach (Vector2Int position in basicWallPositions)
            {
                string neighbourBinaryType = "";
                foreach (Vector2Int direction in Direction2D.CardinalDirectionList)
                {
                    Vector2Int neighbourPosition = position + direction;
                    if (floorPositions.Contains(neighbourPosition))
                    {
                        neighbourBinaryType += "1";
                    }
                    else
                    {
                        neighbourBinaryType += "0";
                    }
                }

                painter.PaintSingleBasicWall(position, neighbourBinaryType, submapGrassTilemap, isSecondIteration, isIsland, cliffsUnderIslandTilemap ? cliffsUnderIslandTilemap : null);
            }
        }
    }
}
