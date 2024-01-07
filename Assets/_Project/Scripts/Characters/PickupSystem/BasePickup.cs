using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickup : MonoBehaviour
{
    [SerializeField] protected PickupTypes type;

    public PickupTypes Type { get => type; set => type = value; }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
