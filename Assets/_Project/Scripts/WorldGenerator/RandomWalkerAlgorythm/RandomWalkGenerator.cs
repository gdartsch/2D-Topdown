using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MatchaIsSpent.WorldGeneration
{
    /// <summary>
    /// This class is used to generate random walk paths.
    /// </summary>
    public static class RandomWalkGenerator
    {
        /// <summary>
        /// Generates a random walk path.
        /// <paramref name="startPosition">The starting position of the path.</paramref>
        /// <paramref name="walkLength">The length of the path.</paramref>
        /// </summary>
        /// <param name="startPosition">The starting position of the path.</param>
        /// <param name="walkLength">The length of the path.</param>
        public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
        {
            HashSet<Vector2Int> path = new HashSet<Vector2Int>
        {
            startPosition
        };

            Vector2Int previousPosition = startPosition;

            for (ushort i = 0; i < walkLength; i++)
            {
                Vector2Int newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();

                path.Add(newPosition);
                previousPosition = newPosition;
            }

            return path;
        }

        /// <summary>
        /// Generates a random walk path.
        /// <paramref name="startPosition">The starting position of the path.</paramref>
        /// <paramref name="walkLength">The length of the path.</paramref>
        /// </summary>
        /// <param name="startPosition">The starting position of the path.</param>
        /// <param name="walkLength">The length of the path.</param>
        public static HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO settings, Vector2Int position)
        {
            Vector2Int currentPosition = position;
            HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

            for (ushort i = 0; i < settings.Iterations; i++)
            {
                HashSet<Vector2Int> path = SimpleRandomWalk(currentPosition, settings.WalkLength);
                floorPositions.UnionWith(path);

                if (settings.StartRandomlyEachIteration)
                    currentPosition = floorPositions.ElementAt(UnityEngine.Random.Range(0, floorPositions.Count));
            }

            return floorPositions;
        }
    }
}
