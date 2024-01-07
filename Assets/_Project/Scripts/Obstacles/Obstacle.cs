using UnityEngine;

namespace MatchaIsSpent.Obstacles
{
    /// <summary>
    /// Obstacles are objects that can block the progression of the player.
    /// </summary>
    public abstract class Obstacle : MonoBehaviour
    {
        [Tooltip("The type of obstacle.")]
        [SerializeField] protected ObstacleType type;

        /// <summary>
        /// The type of obstacle.
        /// </summary>
        public ObstacleType Type { get => type; set => type = value; }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
        }
    }
}