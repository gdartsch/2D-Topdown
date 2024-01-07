using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSystem : BaseStatSystem
{
    public int MaxMana => maxStat;
    public int CurrenMana => currentStat;

    private void Start()
    {
        currentStat = maxStat;
    }

    public void SpendMana(int damage)
    {
        currentStat -= damage;
        OnStatChangedEvent();
    }
}
