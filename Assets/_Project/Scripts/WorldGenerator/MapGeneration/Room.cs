using System.Collections.Generic;
using UnityEngine;

namespace MatchaIsSpent.WorldGeneration
{
    /// <summary>
    /// Class that represents a room in the map
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Center position of the room in the map
        /// </summary>
        public Vector2 RoomCenterPos { get; set; }

        /// <summary>
        /// Flor tiles of the room
        /// </summary>
        public HashSet<Vector2Int> FloorTiles { get; set; } = new HashSet<Vector2Int>();

        /// <summary>
        /// Wall tiles up of the room
        /// </summary>
        public HashSet<Vector2Int> NearWallsTilesUp { get; set; } = new HashSet<Vector2Int>();

        /// <summary>
        /// Wall tiles down of the room
        /// </summary>
        public HashSet<Vector2Int> NearWallsTilesDown { get; set; } = new HashSet<Vector2Int>();

        /// <summary>
        /// Wall tiles left of the room
        /// </summary>
        public HashSet<Vector2Int> NearWallsTilesLeft { get; set; } = new HashSet<Vector2Int>();

        /// <summary>
        /// Wall tiles right of the room
        /// </summary>
        public HashSet<Vector2Int> NearWallsTilesRight { get; set; } = new HashSet<Vector2Int>();

        /// <summary>
        /// Corner tiles of the room
        /// </summary>
        public HashSet<Vector2Int> CornerTiles { get; set; } = new HashSet<Vector2Int>();

        /// <summary>
        /// Inner tiles of the room
        /// </summary>
        public HashSet<Vector2Int> InnerTiles { get; set; } = new HashSet<Vector2Int>();

        /// <summary>
        /// Prop positions of the room
        /// </summary>
        public HashSet<Vector2Int> PropPositions { get; set; } = new HashSet<Vector2Int>();

        /// <summary>
        /// Prop GameObjects of the room
        /// </summary>
        public List<GameObject> PropObjectReferences { get; set; } = new List<GameObject>();

        /// <summary>
        /// Positions that should not be blocked
        /// </summary>
        public List<Vector2Int> PositionsAccessibleFromPath { get; set; } = new List<Vector2Int>();

        /// <summary>
        /// NPCs in the room
        /// </summary>
        public List<GameObject> NPCsInTheRoom { get; set; } = new List<GameObject>();

        /// <summary>
        /// Center position of the room in the map
        /// </summary>
        public Vector2 CenterPosition { get; private set; }

        /// <summary>
        /// Size of the room
        /// </summary>
        public Vector2Int Size { get; private set; }

        /// <summary>
        /// Constructor
        /// <paramref name="centerPosition"/>Center position of the room in the map
        /// <paramref name="floorTiles"/>Floor tiles of the room
        /// <paramref name="size"/>Size of the room
        /// </summary>
        /// <param name="centerPosition"></param>
        /// <param name="floorTiles"></param>
        /// <param name="size"></param>
        /// <returns>
        /// The created Room
        /// </returns>
        public Room(Vector2 centerPosition, HashSet<Vector2Int> floorTiles, Vector2Int size)
        {
            CenterPosition = centerPosition;
            FloorTiles = floorTiles;
            Size = size;
        }
    }
}
