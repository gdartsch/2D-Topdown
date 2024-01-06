using System.Collections.Generic;
using UnityEngine;

namespace MatchaIsSpent.WorldGeneration
{
    /// <summary>
    /// This class contains all possible directions in the game represented as Vector2Int.
    /// </summary>
    public static class Direction2D
    {
        /// <summary>
        /// This list contains all cardinal directions.
        /// </summary>
        public static List<Vector2Int> CardinalDirectionList { get; set; } = new List<Vector2Int>()
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

        /// <summary>
        /// This list contains all diagonal directions.
        /// </summary>
        public static List<Vector2Int> DiagonalDirectionList { get; set; } = new List<Vector2Int>()
    {
        new Vector2Int(1,1), //UpRight
        new Vector2Int(1,-1), //DownRight
        new Vector2Int(-1,-1), //DownLeft
        new Vector2Int(-1,1) //UpLeft
    };

        /// <summary>
        /// This list contains all eight directions.
        /// </summary>
        public static List<Vector2Int> EightDirectionList { get; set; } = new List<Vector2Int>()
    {
        Vector2Int.up,
        new Vector2Int(1,1), //UpRight
        Vector2Int.right,
        new Vector2Int(1,-1), //DownRight
        Vector2Int.down,
        new Vector2Int(-1,-1), //DownLeft
        Vector2Int.left,
        new Vector2Int(-1,1) //UpLeft
    };

        /// <summary>
        /// Gets a random cardinal direction.
        /// </summary>
        public static Vector2Int GetRandomCardinalDirection()
        {
            return CardinalDirectionList[Random.Range(0, CardinalDirectionList.Count)];
        }
    }
}
