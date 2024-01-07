using UnityEngine;
using MatchaIsSpent.InventorySystem;

namespace MatchaIsSpent.Objects
{
    /// <summary>
    /// An object that can be picked up.
    /// </summary>
    public class ChestObject : ItemObject
    {
        [Header("References")]
        [Tooltip("The sprite renderer for the chest.")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [Tooltip("The sprite for the chest when it is open.")]
        [SerializeField] private Sprite openSprite;
        [Tooltip("The sprite for the chest when it is closed.")]
        [SerializeField] private Sprite closedSprite;

        /// <summary>
        /// The items that can be picked up.
        /// </summary>
        private bool isOpen = false;

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Inventory inventory))
            {
                if (!isOpen)
                {
                    foreach (var item in Items)
                    {
                        inventory.AddItem(item);
                    }

                    spriteRenderer.sprite = openSprite;
                    isOpen = true;
                }
            }
        }
    }
}