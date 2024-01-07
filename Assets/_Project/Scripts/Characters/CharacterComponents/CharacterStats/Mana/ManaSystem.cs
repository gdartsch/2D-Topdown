using System.Collections;
using UnityEngine;

public class ManaSystem : BaseStatSystem
{
    [SerializeField] private int manaRecoverRate = 10;
    public int MaxMana => maxStat;
    public int CurrenMana => currentStat;

    private void Start()
    {
        currentStat = maxStat;
        OnStatChangedEvent();
    }

    public void SpendMana(int value)
    {
        currentStat -= value;
        Debug.Log(currentStat + " " + maxStat);
        RestoreManaOverTime();
        OnStatChangedEvent();
    }

    public void RestoreMana(int mana)
    {
        currentStat += mana;
        OnStatChangedEvent();
    }

    private void RestoreManaOverTime()
    {
        StartCoroutine(RestoreManaOverTimeCoroutine());
    }

    private IEnumerator RestoreManaOverTimeCoroutine()
    {
        while (currentStat < maxStat)
        {
            currentStat += manaRecoverRate;
            OnStatChangedEvent();
            yield return new WaitForSeconds(1f);
        }
    }
}
