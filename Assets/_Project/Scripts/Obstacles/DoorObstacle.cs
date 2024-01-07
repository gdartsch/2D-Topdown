using System;
using MatchaIsSpent.InventorySystem;
using UnityEngine;

namespace MatchaIsSpent.Obstacles
{
    /// <summary>
    /// Doors are obstacles that can be opened by using items.
    /// </summary>
    public class DoorObstacle : Obstacle
    {
        [Tooltip("The index of the items needed to open the door.")]
        [SerializeField] private int[] neededObjectsIndex;
        [Tooltip("The scene to load when the door is opened.")]
        [SerializeField] private string sceneToLoad;
        /// <summary>
        /// A list of booleans that determine if the needed items have been used.
        /// </summary>
        private bool[] requirements;

        /// <summary>
        /// Event that is invoked when a door is opened.
        /// </summary>
        public static event Action<string> OnDoorOpen;

        private void Start()
        {
            requirements = new bool[neededObjectsIndex.Length];
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Inventory inventory))
            {
                for (int i = 0; i < neededObjectsIndex.Length; i++)
                {
                    if (inventory.Items.Count > neededObjectsIndex[i])
                    {
                        if (inventory.Items[neededObjectsIndex[i]].Value == neededObjectsIndex[i])
                        {
                            inventory.RemoveItem(inventory.Items[neededObjectsIndex[i]]);
                            requirements[i] = true;
                        }
                    }
                }

                if (CheckRequirements())
                {
                    OnDoorOpen?.Invoke(sceneToLoad);
                }
            }
        }

        /// <summary>
        /// Checks if all the requirements have been met.
        /// </summary>
        /// <returns></returns>
        private bool CheckRequirements()
        {
            foreach (var requirement in requirements)
            {
                if (!requirement)
                {
                    return false;
                }
            }

            return true;
        }

    }
}
