using UnityEngine;

namespace MatchaIsSpent.StatSystem
{
    /// <summary>
    /// Damages objects that can take damage.
    /// </summary>
    public class Damager : MonoBehaviour
    {
        [Tooltip("The amount of damage to deal.")]
        [SerializeField] private int damage = 10;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}
