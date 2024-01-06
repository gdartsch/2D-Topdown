using System.Collections.Generic;
using UnityEngine;

namespace MatchaIsSpent.WorldGeneration
{
    /// <summary>
    /// This class is used to create a graph of a room.
    /// </summary>
    public class RoomGraph
    {
        /// <summary>
        /// This is a list of all the four directions.
        /// </summary>
        public static List<Vector2Int> fourDirections { get; set; } = new List<Vector2Int>()
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

        /// <summary>
        /// Graph of the room.
        /// </summary>
        private Dictionary<Vector2Int, List<Vector2Int>> graph = new Dictionary<Vector2Int, List<Vector2Int>>();

        /// <summary>
        /// This constructor is used to create a graph of a room.
        /// <paramref name="roomFloor"/>: The floor tiles of the room.
        /// </summary>
        /// <param name="roomFloor">The floor tiles of the room.</param>
        /// <returns>A graph of the room.</returns>
        public RoomGraph(HashSet<Vector2Int> roomFloor)
        {
            foreach (Vector2Int tile in roomFloor)
            {
                List<Vector2Int> neighbours = new List<Vector2Int>();
                foreach (Vector2Int direction in fourDirections)
                {
                    Vector2Int newPosition = tile + direction;
                    if (roomFloor.Contains(newPosition))
                    {
                        neighbours.Add(newPosition);
                    }
                }
                graph.Add(tile, neighbours);
            }
        }

        /// <summary>
        /// This method is used to get the neighbours of a tile.
        /// <paramref name="startPosition"/>: The tile to get the neighbours of.
        /// <paramref name="occupiedNodes"/>: The list of occupied nodes.
        /// </summary>
        /// <param name="startPosition">The tile to get the neighbours of.</param>
        /// <param name="neighbourOffsetList">The list of directions to check.</param>
        /// <returns>A list of neighbours.</returns>
        public Dictionary<Vector2Int, Vector2Int> RunBinaryPartitionSpace(Vector2Int startPosition, HashSet<Vector2Int> occupiedNodes)
        {
            Queue<Vector2Int> nodesToVisit = new Queue<Vector2Int>();
            nodesToVisit.Enqueue(startPosition);
            HashSet<Vector2Int> nodesAlreadyVisited = new HashSet<Vector2Int>
        {
            startPosition
        };

            Dictionary<Vector2Int, Vector2Int> map = new Dictionary<Vector2Int, Vector2Int>
        {
            { startPosition, startPosition }
        };

            while (nodesToVisit.Count > 0)
            {
                Vector2Int currentNode = nodesToVisit.Dequeue();

                foreach (Vector2Int neighbour in graph[currentNode])
                {
                    if (!nodesAlreadyVisited.Contains(neighbour) && !occupiedNodes.Contains(neighbour))
                    {
                        nodesToVisit.Enqueue(neighbour);
                        nodesAlreadyVisited.Add(neighbour);
                        map[neighbour] = currentNode;
                    }
                }
            }

            return map;
        }
    }
}
