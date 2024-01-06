using System.Collections.Generic;
using UnityEngine;

namespace MatchaIsSpent.WorldGeneration
{
    /// <summary>
    /// A graph class that can be used to find neighbours of a given vertex.
    /// </summary>
    public class Graph
    {
        /// <summary>
        /// This is a list of all the four directions.
        /// </summary>
        private static List<Vector2Int> neighbourFourDirections = new List<Vector2Int>
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

        /// <summary>
        /// This is a list of all the eight directions.
        /// </summary>
        private static List<Vector2Int> neoighbourEightDirections = new List<Vector2Int>
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left,
        new Vector2Int(1,1), //UpRight
        new Vector2Int(1,-1), //DownRight
        new Vector2Int(-1,-1), //DownLeft
        new Vector2Int(-1,1) //UpLeft
    };

        /// <summary>
        /// Graph of the room.
        /// </summary>
        private List<Vector2Int> graph;

        /// <summary>
        /// This constructor is used to create a graph of a room.
        /// <paramref name="vertices"/>: The vertices of the graph.
        /// </summary>
        /// <param name="vertices">The vertices of the graph.</param>
        /// <returns>A graph of the room.</returns>
        public Graph(IEnumerable<Vector2Int> vertices)
        {
            graph = new List<Vector2Int>(vertices);
        }

        /// <summary>
        /// This method is used to get the neighbours of a tile.
        /// <paramref name="startPosition"/>: The tile to get the neighbours of.
        /// <paramref name="neighbourOffsetList"/>: The list of directions to check.
        /// </summary>
        /// <param name="startPosition">The tile to get the neighbours of.</param>
        /// <param name="neighbourOffsetList">The list of directions to check.</param>
        /// <returns>A list of neighbours.</returns>
        public List<Vector2Int> GetNeighboursFourDirections(Vector2Int startPosition)
        {
            return GetNeighbours(startPosition, neighbourFourDirections);
        }

        /// <summary>
        /// This method is used to get the neighbours of a tile.
        /// <paramref name="startPosition"/>: The tile to get the neighbours of.
        /// <paramref name="neighbourOffsetList"/>: The list of directions to check.
        /// </summary>
        /// <param name="startPosition">The tile to get the neighbours of.</param>
        /// <param name="neighbourOffsetList">The list of directions to check.</param>
        /// <returns>A list of neighbours.</returns>
        public List<Vector2Int> GetNeighboursEightDirections(Vector2Int startPosition)
        {
            return GetNeighbours(startPosition, neoighbourEightDirections);
        }

        /// <summary>
        /// This method is used to get the neighbours of a tile.
        /// <paramref name="startPosition"/>: The tile to get the neighbours of.
        /// <paramref name="neighbourOffsetList"/>: The list of directions to check.
        /// </summary>
        /// <param name="startPosition">The tile to get the neighbours of.</param>
        /// <param name="neighbourOffsetList">The list of directions to check.</param>
        /// <returns>A list of neighbours.</returns>
        private List<Vector2Int> GetNeighbours(Vector2Int startPosition, List<Vector2Int> neighbourOffsetList)
        {
            List<Vector2Int> neighbours = new List<Vector2Int>();

            foreach (Vector2Int neighbourDirection in neighbourOffsetList)
            {
                Vector2Int potentialPeighbour = startPosition + neighbourDirection;
                if (graph.Contains(potentialPeighbour))
                {
                    neighbours.Add(potentialPeighbour);
                }
            }

            return neighbours;
        }
    }
}
