using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchaIsSpent.WorldGeneration
{
    /// <summary>
    /// This class contains all data about the map.
    /// </summary>
    public class MapData : MonoBehaviour
    {
        /// <summary>
        /// This list contains all rooms in the map.
        /// </summary>
        public List<Room> Rooms { get; set; } = new List<Room>();

        /// <summary>
        /// This hashset contains all tiles that are part of the path.
        /// </summary>
        public HashSet<Vector2Int> Path { get; set; } = new HashSet<Vector2Int>();

        /// <summary>
        /// This is a reference to the player.
        /// </summary>
        public GameObject PlayerReference { get; set; }

        /// <summary>
        /// This method resets the map.
        /// </summary>
        public void Reset()
        {
            foreach (Room room in Rooms)
            {
                foreach (GameObject item in room.PropObjectReferences)
                {
                    DestroyImmediate(item);
                }
                foreach (GameObject item in room.NPCsInTheRoom)
                {
                    DestroyImmediate(item);
                }
            }
            Rooms.Clear();
            Rooms = new List<Room>();
            Path = new HashSet<Vector2Int>();
            DestroyImmediate(PlayerReference);
        }
    }
}
