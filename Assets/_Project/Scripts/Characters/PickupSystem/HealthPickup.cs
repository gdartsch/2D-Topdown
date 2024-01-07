using MatchaIsSpent.StatSystem;
using UnityEngine;

namespace MatchaIsSpent.Pickups
{
    /// <summary>
    /// A health pickup restores health to the player.
    /// </summary>
    public class HealthPickup : BasePickup
    {
        [Tooltip("The amount of health to restore.")]
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
}
