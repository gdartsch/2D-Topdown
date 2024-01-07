using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : BasePickup
{
    [SerializeField] private int healthAmount = 10;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.AddHealth(healthAmount);
            Destroy(gameObject);
        }
    }
}
