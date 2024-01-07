using System.Collections;
using UnityEngine;

namespace MatchaIsSpent.StatSystem
{
    /// <summary>
    /// The mana system.
    /// </summary>
    public class ManaSystem : BaseStatSystem
    {
        [Tooltip("The maximum amount of mana.")]
        [SerializeField] private int manaRecoverRate = 10;

        /// <summary>
        /// The maximum amount of mana.
        /// </summary>
        public int MaxMana => maxStat;
        /// <summary>
        /// The current amount of mana.
        /// </summary>
        public int CurrenMana => currentStat;

        private void Start()
        {
            currentStat = maxStat;
            OnStatChangedEvent();
        }

        /// <summary>
        /// Spend mana.
        /// <paramref name="value"/> The amount of mana to spend.
        /// </summary>
        /// <param name="value"></param>
        public void SpendMana(int value)
        {
            currentStat -= value;
            RestoreManaOverTime();
            OnStatChangedEvent();
        }

        /// <summary>
        /// Restore mana.
        /// <paramref name="mana"/> The amount of mana to restore.
        /// </summary>
        /// <param name="mana"></param>
        public void RestoreMana(int mana)
        {
            currentStat += mana;
            OnStatChangedEvent();
        }

        /// <summary>
        /// Restore mana over time.
        /// </summary>
        private void RestoreManaOverTime()
        {
            StartCoroutine(RestoreManaOverTimeCoroutine());
        }

        /// <summary>
        /// Restore mana over time coroutine.
        /// </summary>
        /// <returns></returns>
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
}
