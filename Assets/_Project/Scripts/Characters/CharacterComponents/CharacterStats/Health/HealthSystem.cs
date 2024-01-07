using System;
using System.Collections;
using UnityEngine;

namespace MatchaIsSpent.StatSystem
{
    public enum HealthHolder
    {
        Player,
        Enemy
    }

    /// <summary>
    /// The health system of the character.
    /// </summary>
    public class HealthSystem : BaseStatSystem, IDamageable
    {
        [Header("References")]
        [Tooltip("The damage sound.")]
        [SerializeField] private AudioClip damageSound;
        [Tooltip("The sprite renderer.")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [Tooltip("The audio source.")]
        [SerializeField] private AudioSource audioSource;
        [field: Tooltip("The health holder.")]
        [field: SerializeField] public HealthHolder HealthHolder { get; set; }

        /// <summary>
        /// The maximum amount of health.
        /// </summary>
        public int MaxHealth => maxStat;
        /// <summary>
        /// The current amount of health.
        /// </summary>
        public int CurrentHealth => currentStat;

        /// <summary>
        /// The event that is called when the health holder dies.
        /// </summary>
        public static event Action<HealthSystem> OnDied;

        private void Start()
        {
            currentStat = maxStat;
            OnStatChangedEvent();
        }

        /// <summary>
        /// Take damage.
        /// <paramref name="damage"/> The amount of damage to take.
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage(int damage)
        {
            if (IsDead())
                return;

            currentStat -= damage;
            audioSource.PlayOneShot(damageSound);
            StartCoroutine(FlashSprite());
            OnStatChangedEvent();
        }

        /// <summary>
        /// Flash the sprite.
        /// </summary>
        /// <returns></returns>
        private IEnumerator FlashSprite()
        {
            int flashes = 0;
            while (flashes < 3)
            {
                spriteRenderer.enabled = false;
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.enabled = true;
                yield return new WaitForSeconds(0.1f);
                flashes++;
            }
        }

        /// <summary>
        /// Add health.
        /// <paramref name="health"/> The amount of health to add.
        /// </summary>
        /// <param name="health"></param>
        public void AddHealth(int health)
        {
            currentStat += health;
            OnStatChangedEvent();
        }

        public bool IsDead()
        {
            if (currentStat <= 0)
            {
                OnDied?.Invoke(this);
                Destroy(gameObject);
                return true;
            }

            return false;
        }
    }
}