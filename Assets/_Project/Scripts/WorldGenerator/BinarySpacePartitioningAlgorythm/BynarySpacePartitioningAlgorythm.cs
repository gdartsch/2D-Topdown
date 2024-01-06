using System.Collections.Generic;
using UnityEngine;

namespace MatchaIsSpent.WorldGeneration
{
    /// <summary>
    /// This class contains the Bynary Space Partitioning algorythm.
    /// </summary>
    public static class BynarySpacePartitioningAlgorythm
    {
        /// <summary>
        /// This method generates a list of rooms using the Bynary Space Partitioning algorythm.
        /// <paramref name="spaceToSplit"/> is the space to split.
        /// <paramref name="minWidth"/> is the minimum width of a room.
        /// <paramref name="minHeight"/> is the minimum height of a room.
        /// </summary>
        /// <param name="spaceToSplit"></param>
        /// <param name="minWidth"></param>
        /// <param name="minHeight"></param>
        /// <returns>
        /// A list of the bounds of the rooms.
        /// </returns>
        public static List<BoundsInt> BynarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
        {
            Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
            List<BoundsInt> roomsList = new List<BoundsInt>();
            roomsQueue.Enqueue(spaceToSplit);

            while (roomsQueue.Count > 0)
            {
                BoundsInt room = roomsQueue.Dequeue();
                if (room.size.y >= minHeight && room.size.x >= minWidth)
                {
                    if (UnityEngine.Random.value < 0.5f)
                    {
                        if (room.size.y >= minHeight * 2)
                        {
                            SplitHorizontally(minWidth, roomsQueue, room);
                        }
                        else if (room.size.x >= minWidth * 2)
                        {
                            SplitVertically(minHeight, roomsQueue, room);
                        }
                        else if (room.size.x >= minWidth && room.size.y >= minHeight)
                        {
                            roomsList.Add(room);
                        }
                    }
                    else
                    {
                        if (room.size.x >= minWidth * 2)
                        {
                            SplitVertically(minWidth, roomsQueue, room);
                        }
                        else if (room.size.y >= minHeight * 2)
                        {
                            SplitHorizontally(minHeight, roomsQueue, room);
                        }
                        else if (room.size.x >= minWidth && room.size.y >= minHeight)
                        {
                            roomsList.Add(room);
                        }
                    }
                }
            }
            return roomsList;
        }

        /// <summary>
        /// Splits the room vertically.
        /// <paramref name="minWidht"/> is the minimum width of a room.
        /// <paramref name="roomsQueue"/> is the queue of rooms.
        /// <paramref name="room"/> is the room to split.
        /// </summary>
        /// <param name="minWidht"></param>
        /// <param name="roomsQueue"></param>
        /// <param name="room"></param>
        private static void SplitVertically(int minWidht, Queue<BoundsInt> roomsQueue, BoundsInt room)
        {
            int xSplit = UnityEngine.Random.Range(1, room.size.x);
            BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
            BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
                                    new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
            roomsQueue.Enqueue(room1);
            roomsQueue.Enqueue(room2);
        }

        /// <summary>
        /// Splits the room horizontally.
        /// <paramref name="minHeight"/> is the minimum height of a room.
        /// <paramref name="roomsQueue"/> is the queue of rooms.
        /// <paramref name="room"/> is the room to split.
        /// </summary>
        /// <param name="minHeight"></param>
        /// <param name="roomsQueue"></param>
        /// <param name="room"></param>
        private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
        {
            int ySplit = UnityEngine.Random.Range(1, room.size.y);
            BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
            BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
                                    new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
            roomsQueue.Enqueue(room1);
            roomsQueue.Enqueue(room2);
        }
    }
}
