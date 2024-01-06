using System.Collections.Generic;
using MatchaIsSpent.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MatchaIsSpent.WorldGeneration
{
    /// <summary>
    /// This class is responsible for generating the rooms of the map.
    /// </summary>
    public class RoomGenerator : MonoBehaviour
    {
        [field: Header("Tiles Information")]
        [Tooltip("The list of the tilemaps for the main room")]
        //FloorTiles = 0, ClifEdgeTiles = 1, CliffTiles = 2, WallTiles = 3
        [field: SerializeField] public TileObject[] MainRoomTiles { get; private set; }

        [Tooltip("The list of the tilemaps for the big grass room")]
        //BigGrassTiles = 0, GrassEdgeInnerCornerDownLeft = 1, GrassEdgeInnerCornerDownRight = 2, GrassEdgeDiagonalCornerDownRight = 3
        //GrassEdgeDiagonalCornerDownLeft = 4, GrassEdgeDiagonalCornerUpRight = 5, GrassEdgeDiagonalCornerUpLeft = 6,
        //GrassEdgeFull = 7, GrassEdgeBottom = 8, GrassEdgeTop = 9, GrassEdgeRight = 10, GrassEdgeLeft = 11
        [field: SerializeField] public TileObject[] BigGrassRoomTiles { get; private set; }

        [Tooltip("The list of the tilemaps for the small grass room")]
        //SmallGrassTiles = 0, SmallGrassEdgeInnerCornerDownLeft = 1, SmallGrassEdgeInnerCornerDownRight = 2, SmallGrassEdgeDiagonalCornerDownRight = 3
        //SmallGrassEdgeDiagonalCornerDownLeft = 4, SmallGrassEdgeDiagonalCornerUpRight = 5, SmallGrassEdgeDiagonalCornerUpLeft = 6,
        //SmallGrassEdgeFull = 7, SmallGrassEdgeBottom = 8, SmallGrassEdgeTop = 9, SmallGrassEdgeRight = 10, SmallGrassEdgeLeft = 11
        [field: SerializeField] public TileObject[] SmallGrassRoomTiles { get; private set; }

        [Tooltip("The list of the tilemaps for the island room")]
        //IslandTiles = 0, IslandEdgeInnerCornerDownLeft = 1, IslandEdgeInnerCornerDownRight = 2, IslandEdgeDiagonalCornerDownRight = 3
        //IslandEdgeDiagonalCornerDownLeft = 4, IslandEdgeDiagonalCornerUpRight = 5, IslandEdgeDiagonalCornerUpLeft = 6,
        //IslandEdgeFull = 7, IslandEdgeBottom = 8, IslandEdgeTop = 9, IslandEdgeRight = 10, IslandEdgeLeft = 11
        [field: SerializeField] public TileObject[] IslandRoomTiles { get; private set; }

        [field: Space(1), Header("References")]
        [Tooltip("The world generation settings")]
        [SerializeField] private WorldGenerationSettings worldGenerationSettings = null;
        [Tooltip("The edge generator")]
        [SerializeField] private EdgeGenerator edgeGenerator = null;

        /// <summary>
        /// Generates the rooms of the map.
        /// <paramref name="submapContainer"/> The container of the submaps.
        /// </summary>
        /// <param name="submapContainer"></param>
        public void GenerateRooms(GameObject[] submapContainer)
        {
            GameObject[] wallColliders = new GameObject[worldGenerationSettings.Submaps];
            GameObject[] cliffMap = new GameObject[worldGenerationSettings.Submaps];

            for (int i = 0; i < worldGenerationSettings.Submaps; i++)
            {
                GameObject submap = new GameObject($"Submap {i}");
                submap.transform.parent = worldGenerationSettings.WorldMap.transform;

                worldGenerationSettings.randomTileIndex = UnityEngine.Random.Range(0, MainRoomTiles[0].Tiles.Length); //Floor tiles

                submapContainer[i] = submap;

                Vector2Int submapPosition =
                    new Vector2Int(
                        (int)submap.transform.position.x, (int)submap.transform.position.y) + Vector2Int.down * worldGenerationSettings.RoomSize.y / worldGenerationSettings.Submaps * i;

                Tilemap tilemap = submap.AddComponent<Tilemap>();
                worldGenerationSettings.TilesDataManager.Tilemaps.Add(tilemap);

                TilemapRenderer tilemapRenderer = submap.AddComponent<TilemapRenderer>();

                worldGenerationSettings.MapData.Rooms.Add(
                    GenerateRectangularRoomAt(
                        submapPosition, new Vector2Int(worldGenerationSettings.RoomSize.x, (int)(worldGenerationSettings.RoomSize.y / worldGenerationSettings.Submaps)), tilemap));

                submapContainer[i].transform.position = new Vector3(submapPosition.x, submapPosition.y, 0);
                tilemapRenderer.sortingOrder = 0;
                tilemapRenderer.sortingLayerID = SortingLayer.NameToID("Background");
                wallColliders[i] = new GameObject($"Wall Colliders {i}");
                wallColliders[i].transform.parent = submapContainer[i].transform;

                Tilemap colMap = wallColliders[i].AddComponent<Tilemap>();
                worldGenerationSettings.TilesDataManager.Tilemaps.Add(colMap);

                TilemapRenderer colMapRenderer = wallColliders[i].AddComponent<TilemapRenderer>();
                colMapRenderer.sortingOrder = 3;
                colMapRenderer.sortingLayerID = SortingLayer.NameToID("Background");

                TilemapCollider2D tilemapCollider2D = wallColliders[i].AddComponent<TilemapCollider2D>();
                tilemapCollider2D.usedByComposite = true;
                tilemapCollider2D.isTrigger = false;

                Rigidbody2D rigidbody2D = wallColliders[i].AddComponent<Rigidbody2D>();
                rigidbody2D.bodyType = RigidbodyType2D.Static;

                CompositeCollider2D compositeCollider2D = wallColliders[i].AddComponent<CompositeCollider2D>();
                compositeCollider2D.isTrigger = false;

                cliffMap[i] = new GameObject($"Cliff Map {i}");
                cliffMap[i].transform.parent = submapContainer[i].transform;
                cliffMap[i].transform.position = new Vector3(cliffMap[i].transform.position.x, cliffMap[i].transform.position.y + 1, 0);

                Tilemap cliffTilemap = cliffMap[i].AddComponent<Tilemap>();
                worldGenerationSettings.TilesDataManager.Tilemaps.Add(cliffTilemap);

                TilemapRenderer cliffTilemapRenderer = cliffMap[i].AddComponent<TilemapRenderer>();
                cliffTilemapRenderer.sortingOrder = 4;
                cliffTilemapRenderer.sortingLayerID = SortingLayer.NameToID("Background");

                TilemapCollider2D cliffTilemapCollider2D = cliffMap[i].AddComponent<TilemapCollider2D>();
                cliffTilemapCollider2D.usedByComposite = true;
                cliffTilemapCollider2D.isTrigger = true;

                Rigidbody2D cliffRigidbody2D = cliffMap[i].AddComponent<Rigidbody2D>();
                cliffRigidbody2D.bodyType = RigidbodyType2D.Static;

                CompositeCollider2D cliffCompositeCollider2D = cliffMap[i].AddComponent<CompositeCollider2D>();
                cliffCompositeCollider2D.isTrigger = true;

                edgeGenerator.GenerateMapCollider(colMap, cliffTilemap, worldGenerationSettings.MapData.Rooms[i], i == worldGenerationSettings.Submaps - 1, i == 0);
            }
        }

        /// <summary>
        /// Generates a rectangular room at the given position.
        /// <paramref name="roomCenterPosition"/> The center position of the room.
        /// <paramref name="roomSize"/> The size of the room.
        /// <paramref name="room"/> The tilemap of the room.
        /// </summary>
        /// <param name="roomCenterPosition"></param>
        /// <param name="roomSize"></param>
        /// <param name="room"></param>
        /// <returns>The room generated.</returns>
        private Room GenerateRectangularRoomAt(Vector2 roomCenterPosition, Vector2Int roomSize, Tilemap room)
        {
            Vector2Int half = roomSize / 2;

            HashSet<Vector2Int> roomTiles = new HashSet<Vector2Int>();

            for (int x = -half.x; x <= half.x; x++)
            {
                for (int y = -half.y; y <= half.y; y++)
                {
                    Vector3Int positionInt = room.WorldToCell(roomCenterPosition + new Vector2Int(x, y));
                    roomTiles.Add((Vector2Int)positionInt);
                }
            }

            TileBase tile = MainRoomTiles[0].Tiles[worldGenerationSettings.randomTileIndex]; //Floor Tiles

            for (int x = -half.x; x < half.x; x++)
            {
                for (int y = -half.y; y < half.y; y++)
                {
                    Vector2 position = new Vector2Int(x, y);
                    Vector3Int positionInt = room.WorldToCell(position);
                    room.SetTile(positionInt, tile);
                }
            }

            return new Room(roomCenterPosition, roomTiles, roomSize);
        }

        /// <summary>
        /// Generates the grass and the islands of the map.
        /// <paramref name="submapContainer"/> The container of the submaps.
        /// </summary>
        /// <param name="submapContainer"></param>
        public void GenerateGrassAndIslands(GameObject[] submapContainer)
        {

            HashSet<Vector2Int>[] bigGrassPositions = new HashSet<Vector2Int>[worldGenerationSettings.Submaps];
            HashSet<Vector2Int>[] smallGrassPositions = new HashSet<Vector2Int>[worldGenerationSettings.Submaps];

            HashSet<Vector2Int> bigGrassFloorTiles = new HashSet<Vector2Int>();
            HashSet<Vector2Int> smallGrassFloorTiles = new HashSet<Vector2Int>();

            int j = 1;
            foreach (Room room in worldGenerationSettings.MapData.Rooms)
            {
                HashSet<Vector2Int> islandFloorTiles;
                HashSet<Vector2Int>[] islandPositions;
                GenerateGrass(submapContainer, bigGrassPositions, smallGrassPositions, bigGrassFloorTiles, smallGrassFloorTiles, j, room, out islandFloorTiles, out islandPositions);
                GenerateIslands(submapContainer, j, room, islandFloorTiles, islandPositions);

                j++;
            }
        }

        /// <summary>
        /// Generates the islands of the map.
        /// <paramref name="submapContainer"/> The container of the submaps.
        /// <paramref name="j"/> The index of the submap.
        /// <paramref name="room"/> The room of the submap.
        /// <paramref name="islandFloorTiles"/> The floor tiles of the island.
        /// <paramref name="islandPositions"/> The positions of the island.
        /// </summary>
        /// <param name="submapContainer"></param>
        /// <param name="j"></param>
        /// <param name="room"></param>
        /// <param name="islandFloorTiles"></param>
        /// <param name="islandPositions"></param>
        private void GenerateIslands(GameObject[] submapContainer, int j, Room room, HashSet<Vector2Int> islandFloorTiles, HashSet<Vector2Int>[] islandPositions)
        {
            GameObject newIsland = new GameObject($"Submap Island {j}");
            newIsland.transform.parent = worldGenerationSettings.GrassOverMap.transform;

            Tilemap islandFloorTilemap = newIsland.AddComponent<Tilemap>();
            worldGenerationSettings.TilesDataManager.Tilemaps.Add(islandFloorTilemap);

            TilemapRenderer islandFloorTilemapRenderer = newIsland.AddComponent<TilemapRenderer>();
            islandFloorTilemapRenderer.sortingOrder = 3;
            islandFloorTilemapRenderer.sortingLayerID = SortingLayer.NameToID("Background");

            TilemapCollider2D islandCollider = newIsland.AddComponent<TilemapCollider2D>();
            islandCollider.usedByComposite = true;
            islandCollider.isTrigger = false;

            Rigidbody2D islandRigidbody = newIsland.AddComponent<Rigidbody2D>();
            islandRigidbody.bodyType = RigidbodyType2D.Static;

            CompositeCollider2D islandCompositeCollider = newIsland.AddComponent<CompositeCollider2D>();
            islandCompositeCollider.isTrigger = false;

            GameObject cliffsUnderIsland = new GameObject($"Cliffs Under Island {j}");
            cliffsUnderIsland.transform.parent = worldGenerationSettings.GrassOverMap.transform;

            Tilemap cliffsUnderIslandTilemap = cliffsUnderIsland.AddComponent<Tilemap>();
            worldGenerationSettings.TilesDataManager.Tilemaps.Add(cliffsUnderIslandTilemap);

            TilemapRenderer cliffsUnderIslandTilemapRenderer = cliffsUnderIsland.AddComponent<TilemapRenderer>();
            cliffsUnderIslandTilemapRenderer.sortingOrder = 4;
            cliffsUnderIslandTilemapRenderer.sortingLayerID = SortingLayer.NameToID("Background");

            TilemapCollider2D cliffsUnderIslandCollider = cliffsUnderIsland.AddComponent<TilemapCollider2D>();
            cliffsUnderIslandCollider.usedByComposite = true;
            cliffsUnderIslandCollider.isTrigger = false;

            Rigidbody2D cliffsUnderIslandRigidbody = cliffsUnderIsland.AddComponent<Rigidbody2D>();
            cliffsUnderIslandRigidbody.bodyType = RigidbodyType2D.Static;

            CompositeCollider2D cliffsUnderIslandCompositeCollider = cliffsUnderIsland.AddComponent<CompositeCollider2D>();
            cliffsUnderIslandCompositeCollider.isTrigger = false;

            HashSet<Vector2Int> smallIslandRoom = CreateIslandRooms(islandFloorTilemap,
                                                  new Vector2Int((int)submapContainer[j - 1].transform.position.x, (int)submapContainer[j - 1].transform.position.y),
                                                  room.Size,
                                                  worldGenerationSettings.SmallIslandRandomWalkSettings, true, true);

            islandFloorTiles.UnionWith(smallIslandRoom);

            islandPositions[j - 1] = smallIslandRoom;

            edgeGenerator.CreateBorders(islandPositions[j - 1], islandFloorTilemap, true, true, cliffsUnderIslandTilemap);
        }

        /// <summary>
        /// Generates the grass of the map.
        /// <paramref name="submapContainer"/> The container of the submaps.
        /// <paramref name="bigGrassPositions"/> The positions of the big grass.
        /// <paramref name="smallGrassPositions"/> The positions of the small grass.
        /// <paramref name="bigGrassFloorTiles"/> The floor tiles of the big grass.
        /// <paramref name="smallGrassFloorTiles"/> The floor tiles of the small grass.
        /// <paramref name="j"/> The index of the submap.
        /// <paramref name="room"/> The room of the submap.
        /// <paramref name="islandFloorTiles"/> The floor tiles of the island.
        /// <paramref name="islandPositions"/> The positions of the island.
        /// </summary>
        /// <param name="submapContainer"></param>
        /// <param name="bigGrassPositions"></param>
        /// <param name="smallGrassPositions"></param>
        /// <param name="bigGrassFloorTiles"></param>
        /// <param name="smallGrassFloorTiles"></param>
        /// <param name="j"></param>
        /// <param name="room"></param>
        /// <param name="islandFloorTiles"></param>
        /// <param name="islandPositions"></param>
        private void GenerateGrass(GameObject[] submapContainer, HashSet<Vector2Int>[] bigGrassPositions, HashSet<Vector2Int>[] smallGrassPositions,
                                   HashSet<Vector2Int> bigGrassFloorTiles, HashSet<Vector2Int> smallGrassFloorTiles, int j, Room room, out HashSet<Vector2Int> islandFloorTiles,
                                   out HashSet<Vector2Int>[] islandPositions)
        {
            worldGenerationSettings.randomTileIndex = UnityEngine.Random.Range(0, BigGrassRoomTiles[0].Tiles.Length); //Big Grass Tiles

            GameObject submapGrass = new GameObject($"Submap Grass {j}");
            submapGrass.transform.parent = worldGenerationSettings.GrassMap.transform;

            Tilemap submapGrassTilemap = submapGrass.AddComponent<Tilemap>();
            worldGenerationSettings.TilesDataManager.Tilemaps.Add(submapGrassTilemap);

            TilemapRenderer submapGrassTilemapRenderer = submapGrass.AddComponent<TilemapRenderer>();
            submapGrassTilemapRenderer.sortingOrder = 1;
            submapGrassTilemapRenderer.sortingLayerID = SortingLayer.NameToID("Background");

            GameObject submapGrassOver = new GameObject($"Submap Grass Over {j}");
            submapGrassOver.transform.parent = worldGenerationSettings.GrassOverMap.transform;

            Tilemap submapGrassOverTilemap = submapGrassOver.AddComponent<Tilemap>();
            worldGenerationSettings.TilesDataManager.Tilemaps.Add(submapGrassOverTilemap);

            TilemapRenderer submapGrassOverTilemapRenderer = submapGrassOver.AddComponent<TilemapRenderer>();
            submapGrassOverTilemapRenderer.sortingOrder = 2;
            submapGrassOverTilemapRenderer.sortingLayerID = SortingLayer.NameToID("Background");

            HashSet<Vector2Int> grassRoom = CreateGrassRooms(submapGrassTilemap,
                                             new Vector2Int((int)submapContainer[j - 1].transform.position.x, (int)submapContainer[j - 1].transform.position.y),
                                             room.Size,
                                             worldGenerationSettings.BigGrassRandomWalkSettings);

            HashSet<Vector2Int> smallGrassRoom = CreateGrassRooms(submapGrassOverTilemap,
                                                  new Vector2Int((int)submapContainer[j - 1].transform.position.x, (int)submapContainer[j - 1].transform.position.y),
                                                  room.Size,
                                                  worldGenerationSettings.SmallGrassRandomWalkSettings, true);

            bigGrassFloorTiles.UnionWith(grassRoom);
            smallGrassFloorTiles.UnionWith(smallGrassRoom);

            bigGrassPositions[j - 1] = grassRoom;
            smallGrassPositions[j - 1] = smallGrassRoom;

            edgeGenerator.CreateBorders(bigGrassPositions[j - 1], submapGrassTilemap);
            edgeGenerator.CreateBorders(smallGrassPositions[j - 1], submapGrassOverTilemap, true);

            islandFloorTiles = new HashSet<Vector2Int>();
            islandPositions = new HashSet<Vector2Int>[worldGenerationSettings.Submaps];
        }

        /// <summary>
        /// Creates the island rooms.
        /// <paramref name="grassSubmap"/> The submap of the grass.
        /// <paramref name="startPosition"/> The start position of the island.
        /// <paramref name="size"/> The size of the island.
        /// <paramref name="randomWalkSettings"/> The settings of the random walk.
        /// <paramref name="secondIteration"/> If the island is created on the second iteration.
        /// <paramref name="isIsland"/> If the island is created on the island.
        /// </summary>
        /// <param name="grassSubmap"></param>
        /// <param name="startPosition"></param>
        /// <param name="size"></param>
        /// <param name="randomWalkSettings"></param>
        /// <param name="secondIteration"></param>
        /// <param name="isIsland"></param>
        /// <returns>The floor tiles of the island.</returns>
        private HashSet<Vector2Int> CreateIslandRooms(Tilemap grassSubmap, Vector2Int startPosition, Vector2Int size,
                                                      SimpleRandomWalkSO randomWalkSettings, bool secondIteration = false, bool isIsland = false)
        {
            List<BoundsInt> roomsList = BynarySpacePartitioningAlgorythm.BynarySpacePartitioning(
                new BoundsInt(
                    (Vector3Int)startPosition - (Vector3Int)size / 2, new Vector3Int(size.x, size.y, 0)), worldGenerationSettings.MinRoomWidth, worldGenerationSettings.MinRoomHeight);

            HashSet<Vector2Int> floor = CreateRoomsRandomly(roomsList, randomWalkSettings);

            List<Vector2Int> roomsCenters = new List<Vector2Int>();

            foreach (BoundsInt room in roomsList)
                roomsCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));

            TileBase tile =
                isIsland ? IslandRoomTiles[0].Tiles[worldGenerationSettings.randomTileIndex] :
                    secondIteration ? SmallGrassRoomTiles[0].Tiles[worldGenerationSettings.randomTileIndex] :
                        BigGrassRoomTiles[0].Tiles[worldGenerationSettings.randomTileIndex];

            foreach (Vector2Int grassPos in floor)
            {
                grassSubmap.SetTile((Vector3Int)grassPos, tile);
                if (UnityEngine.Random.value < 0.3f)
                {
                    GameObject bushOverIsland = new GameObject("Bush Over Island");
                    bushOverIsland.transform.parent = worldGenerationSettings.GrassOverMap.transform;
                    Tilemap bushOverIslandTilemap = bushOverIsland.AddComponent<Tilemap>();
                    worldGenerationSettings.TilesDataManager.Tilemaps.Add(bushOverIslandTilemap);
                    TilemapRenderer bushOverIslandTilemapRenderer = bushOverIsland.AddComponent<TilemapRenderer>();
                    bushOverIslandTilemapRenderer.sortingOrder = 5;
                    bushOverIslandTilemapRenderer.sortingLayerID = SortingLayer.NameToID("Background");
                    bushOverIslandTilemap.SetTile((Vector3Int)grassPos, BigGrassRoomTiles[0].Tiles[worldGenerationSettings.randomTileIndex]);
                }
            }

            return floor;
        }

        /// <summary>
        /// Creates the grass rooms.
        /// <paramref name="grassSubmap"/> The submap of the grass.
        /// <paramref name="startPosition"/> The start position of the grass.
        /// <paramref name="size"/> The size of the grass.
        /// <paramref name="randomWalkSettings"/> The settings of the random walk.
        /// <paramref name="secondIteration"/> If the grass is created on the second iteration.
        /// </summary>
        /// <param name="grassSubmap"></param>
        /// <param name="startPosition"></param>
        /// <param name="size"></param>
        /// <param name="randomWalkSettings"></param>
        /// <param name="secondIteration"></param>
        /// <returns>
        /// The positions of the grass.
        /// </returns>
        private HashSet<Vector2Int> CreateGrassRooms(Tilemap grassSubmap, Vector2Int startPosition, Vector2Int size,
                                                     SimpleRandomWalkSO randomWalkSettings, bool secondIteration = false)
        {
            List<BoundsInt> roomsList = BynarySpacePartitioningAlgorythm.BynarySpacePartitioning(
                new BoundsInt((Vector3Int)startPosition - (Vector3Int)size / 2,
                new Vector3Int(size.x, size.y, 0)),
                worldGenerationSettings.MinRoomWidth, worldGenerationSettings.MinRoomHeight);

            HashSet<Vector2Int> floor = CreateRoomsRandomly(roomsList, randomWalkSettings);

            List<Vector2Int> roomsCenters = new List<Vector2Int>();

            foreach (BoundsInt room in roomsList)
                roomsCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));


            TileBase tile =
                secondIteration ? SmallGrassRoomTiles[0].Tiles[worldGenerationSettings.randomTileIndex] : BigGrassRoomTiles[0].Tiles[worldGenerationSettings.randomTileIndex];

            foreach (Vector2Int grassPos in floor)
                grassSubmap.SetTile((Vector3Int)grassPos, tile);

            return floor;
        }

        /// <summary>
        /// Creates the rooms randomly.
        /// <paramref name="roomsList"/> The list of the rooms.
        /// <paramref name="randomWalkSettings"/> The settings of the random walk.
        /// </summary>
        /// <param name="roomsList"></param>
        /// <param name="randomWalkSettings"></param>
        /// <returns>
        /// A list of the positions of the floor tiles of the rooms.
        /// </returns>
        private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList, SimpleRandomWalkSO randomWalkSettings)
        {
            HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

            for (int i = 0; i < roomsList.Count; i++)
            {
                BoundsInt roomBounds = roomsList[i];
                Vector2Int roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
                HashSet<Vector2Int> roomFloor = RandomWalkGenerator.RunRandomWalk(randomWalkSettings, roomCenter);

                foreach (Vector2Int position in roomFloor)
                {
                    if (position.x >= (roomBounds.xMin + worldGenerationSettings.Offset) && position.x <= (roomBounds.xMax - worldGenerationSettings.Offset) &&
                        position.y >= roomBounds.yMin - worldGenerationSettings.Offset && position.y <= (roomBounds.yMax - worldGenerationSettings.Offset))
                        floor.Add(position);
                }
            }

            return floor;
        }
    }
}
