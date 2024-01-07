using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] protected Item[] items;

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
