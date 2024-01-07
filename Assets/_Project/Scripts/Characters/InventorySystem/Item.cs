using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "MatchaIsSpent/Inventory System/Item")]
public class Item : ScriptableObject
{
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public int Value { get; set; }
    [field: SerializeField] public Sprite Icon { get; set; }
    [field: SerializeField] public ItemType Type { get; set; }
}
