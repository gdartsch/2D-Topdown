using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestObject : ItemObject
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private Sprite closedSprite;

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
