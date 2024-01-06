using UnityEngine;

namespace MatchaIsSpent.WorldGeneration
{
    /// <summary>
    /// This class contains data for the SimpleRandomWalk algorithm.
    /// </summary>
    [CreateAssetMenu(fileName = "SimpleRandomWalkParameters", menuName = "MatchaIsSpent/WorldGeneration/SimpleRandomWalkData", order = 1)]
    public class SimpleRandomWalkSO : ScriptableObject
    {
        [Tooltip("The number of iterations the algorithm will run.")]
        [field: SerializeField] public int Iterations { get; private set; } = 10;
        [Tooltip("The length of each walk.")]
        [field: SerializeField] public int WalkLength { get; private set; } = 10;
        [Tooltip("The chance of the algorithm to change direction.")]
        [field: SerializeField] public bool StartRandomlyEachIteration { get; private set; } = true;
    }
}
