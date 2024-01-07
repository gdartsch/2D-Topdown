
namespace MatchaIsSpent.CharactersStateSystem
{
    /// <summary>
    /// Interface for the land state.
    /// </summary>
    public interface ILand
    {
        void Land();
    }

    /// <summary>
    /// Interface for the hook ability.
    /// </summary>
    public interface IHook
    {
        void LaunchHook();
    }

    /// <summary>
    /// Interface for the attack ability.
    /// </summary>
    public interface IAttack
    {
        void Attack();
    }
    /// <summary>
    /// Interface for the teleport ability.
    /// </summary>
    public interface ITeleport
    {
        void Teleport();
    }
}
