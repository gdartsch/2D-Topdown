using MatchaIsSpent.StatSystem;
using UnityEngine;

namespace MatchaIsSpent.Pickups
{
    /// <summary>
    /// A mana pickup restores mana to the player.
    /// </summary>
    public class ManaPickup : BasePickup
    {
        [Tooltip("The amount of mana to restore.")]
        [SerializeField] private int manaAmount = 10;

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ManaSystem manaSystem))
            {
                manaSystem.RestoreMana(manaAmount);
                Destroy(gameObject);
            }
        }
    }
}
