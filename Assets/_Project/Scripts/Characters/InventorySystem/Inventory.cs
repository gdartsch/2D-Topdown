using System.Collections.Generic;
using MatchaIsSpent.Objects;
using UnityEngine;

namespace MatchaIsSpent.InventorySystem
{
    /// <summary>
    /// The inventory of the player.
    /// </summary>
    public class Inventory : MonoBehaviour
    {
        [Tooltip("The items in the inventory.")]
        [SerializeField] private List<Item> items = new List<Item>();

        /// <summary>
        /// The items in the inventory.
        /// </summary>
        public List<Item> Items { get => items; set => items = value; }

        /// <summary>
        /// Adds an item to the inventory.
        /// <paramref name="item"/> The item to add.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        /// <summary>
        /// Removes an item from the inventory.
        /// <paramref name="item"/> The item to remove.
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }
    }
}
