using System;
using UnityEngine;

namespace MatchaIsSpent.StatSystem
{
    public class BaseStatSystem : MonoBehaviour
    {
        [SerializeField] protected int maxStat = 100;
        [SerializeField] protected int currentStat = 100;
        [SerializeField] protected StatType statType;

        public static event Action<int, int, StatType> OnStatChanged;

        protected void OnStatChangedEvent()
        {
            OnStatChanged?.Invoke(currentStat, maxStat, statType);
        }
    }
}