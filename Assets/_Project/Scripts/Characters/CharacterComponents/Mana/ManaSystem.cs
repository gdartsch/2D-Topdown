using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSystem : MonoBehaviour
{
    [SerializeField] private float mana = 100f;

    public float Mana { get => mana; set => mana = value; }

    public void SpendMana(float amount)
    {
        Mana -= amount;
    }
}
