using UnityEngine;

namespace MatchaIsSpent.Pickups
{
    /// <summary>
    /// The base class for all pickups.
    /// </summary>
    public class BasePickup : MonoBehaviour
    {
        [Tooltip("The type of pickup.")]
        [SerializeField] protected PickupTypes type;

        /// <summary>
        /// The type of pickup.
        /// </summary>
        public PickupTypes Type { get => type; set => type = value; }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
        }
    }
}
