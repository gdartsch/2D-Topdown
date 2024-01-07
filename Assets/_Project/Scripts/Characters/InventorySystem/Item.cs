using UnityEngine;

namespace MatchaIsSpent.Objects
{
    /// <summary>
    /// An item that can be picked up.
    /// </summary>
    [CreateAssetMenu(fileName = "NewItem", menuName = "MatchaIsSpent/Inventory System/Item")]
    public class Item : ScriptableObject
    {
        [Tooltip("The name of the item.")]
        [field: SerializeField] public string Name { get; set; }
        [Tooltip("The value of the item.")]
        [field: SerializeField] public int Value { get; set; }
        [Tooltip("The icon of the item.")]
        [field: SerializeField] public Sprite Icon { get; set; }
        [Tooltip("The type of item.")]
        [field: SerializeField] public ItemType Type { get; set; }
    }
}
