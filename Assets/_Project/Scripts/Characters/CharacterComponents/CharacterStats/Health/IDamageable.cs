namespace MatchaIsSpent.StatSystem
{
    /// <summary>
    /// An interface for objects that can take damage.
    /// </summary>
    public interface IDamageable
    {
        /// <summary>
        /// Take damage.
        /// <paramref name="damage"/> The amount of damage to take.
        /// </summary>
        /// <param name="damage"></param>
        void TakeDamage(int damage);
    }
}
