using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup : BasePickup
{
    [SerializeField] private int manaAmount = 10;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ManaSystem manaSystem))
        {
            manaSystem.RestoreMana(manaAmount);
            Destroy(gameObject);
        }
    }
}
