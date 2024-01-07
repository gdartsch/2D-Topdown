using MatchaIsSpent.InventorySystem;
using UnityEngine;

namespace MatchaIsSpent.Objects
{
    /// <summary>
    /// An object that can be picked up.
    /// </summary>
    public class ItemObject : MonoBehaviour
    {
        [Tooltip("The items that can be picked up.")]
        [SerializeField] protected Item[] items;

        /// <summary>
        /// The items that can be picked up.
        /// </summary>
        public Item[] Items { get => items; set => items = value; }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Inventory inventory))
            {
                foreach (var item in Items)
                {
                    inventory.AddItem(item);
                }
            }
        }
    }
}
