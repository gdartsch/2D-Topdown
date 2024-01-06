using System.Collections.Generic;
using UnityEngine;

namespace MatchaIsSpent.WorldGeneration
{
    /// <summary>
    /// This class contains algorythms for generating random paths.
    /// </summary>
    public static class RandomWalkAlgorythm
    {
        /// <summary>
        /// Generates a random walk path.
        /// <paramref name="startPosition"/>: The starting position of the path.
        /// <paramref name="walkLength"/>: The length of the path.
        /// </summary>
        /// <param name="startPosition">The starting position of the path.</param>
        /// <param name="walkLength">The length of the path.</param>
        /// <returns>
        /// A hashset of positions that make up the path.
        /// </returns>
        public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
        {
            HashSet<Vector2Int> path = new HashSet<Vector2Int>
        {
            startPosition
        };

            Vector2Int previousPosition = startPosition;

            for (short i = 0; i < walkLength; i++)
            {
                Vector2Int newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();

                path.Add(newPosition);
                previousPosition = newPosition;
            }

            return path;
        }

        /// <summary>
        /// Generates a random walk path.
        /// <paramref name="startPosition"/>: The starting position of the path.
        /// <paramref name="walkLength"/>: The length of the path.
        /// </summary>
        /// <param name="startPosition">The starting position of the path.</param>
        /// <param name="walkLength">The length of the path.</param>
        /// <returns>
        /// A list of positions that make up the path.
        /// </returns>
        public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
        {
            List<Vector2Int> corridor = new List<Vector2Int>();
            Vector2Int direction = Direction2D.GetRandomCardinalDirection();
            Vector2Int currentPosition = startPosition;
            corridor.Add(currentPosition);

            for (short i = 0; i < corridorLength; i++)
            {
                currentPosition += direction;
                corridor.Add(currentPosition);
            }

            return corridor;
        }
    }
}
