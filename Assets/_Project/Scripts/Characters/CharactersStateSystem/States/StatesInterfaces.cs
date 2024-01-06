
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
    /// Interface for the teleport ability.
    /// </summary>
    public interface IHook
    {
        void LaunchHook();
    }

    public interface IMeleeAttack
    {
        void MeleeAttack();
    }
}
