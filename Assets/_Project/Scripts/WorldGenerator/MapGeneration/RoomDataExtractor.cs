using UnityEngine;
using UnityEngine.Events;

namespace MatchaIsSpent.WorldGeneration
{
    /// <summary>
    /// Extracts data from rooms
    /// </summary>
    public class RoomDataExtractor : MonoBehaviour
    {
        /// <summary>
        /// Map data
        /// </summary>
        [SerializeField] private MapData mapData;

        /// <summary>
        /// Event invoked when room processing is finished
        /// </summary>
        public UnityEvent OnFinishedRoomProcessing;

        /// <summary>
        /// Extracts data from rooms
        /// </summary>
        public void ProcessRooms()
        {
            if (mapData == null) return;

            foreach (Room room in mapData.Rooms)
            {
                foreach (Vector2Int tilePosition in room.FloorTiles)
                {
                    int neighboursCount = 4;

                    if (room.FloorTiles.Contains(tilePosition + Vector2Int.up) == false)
                    {

                        room.NearWallsTilesUp.Add(tilePosition);
                        neighboursCount--;
                    }
                    if (room.FloorTiles.Contains(tilePosition + Vector2Int.down) == false)
                    {
                        room.NearWallsTilesDown.Add(tilePosition);
                        neighboursCount--;
                    }
                    if (room.FloorTiles.Contains(tilePosition + Vector2Int.right) == false)
                    {
                        room.NearWallsTilesRight.Add(tilePosition);
                        neighboursCount--;
                    }
                    if (room.FloorTiles.Contains(tilePosition + Vector2Int.left) == false)
                    {
                        room.NearWallsTilesLeft.Add(tilePosition);
                        neighboursCount--;
                    }

                    //find corners
                    if (neighboursCount <= 2)
                        room.CornerTiles.Add(tilePosition);

                    if (neighboursCount == 4)
                        room.InnerTiles.Add(tilePosition);
                }

                room.NearWallsTilesUp.ExceptWith(room.CornerTiles);
                room.NearWallsTilesDown.ExceptWith(room.CornerTiles);
                room.NearWallsTilesLeft.ExceptWith(room.CornerTiles);
                room.NearWallsTilesRight.ExceptWith(room.CornerTiles);
            }

            OnFinishedRoomProcessing?.Invoke();
        }
    }
}
