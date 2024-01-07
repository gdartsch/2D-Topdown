using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateUIListener : MonoBehaviour
{
    [SerializeField] private Slider stateUI;
    [SerializeField] protected StatType statType;

    private void OnEnable()
    {
        BaseStatSystem.OnStatChanged += OnStatChanged;
    }

    private void OnDisable()
    {
        BaseStatSystem.OnStatChanged -= OnStatChanged;
    }

    private void OnStatChanged(int currentStat, int maxStat, StatType statType)
    {
        if (this.statType != statType) return;

        stateUI.value = (float)currentStat / maxStat;
    }
}
