
using MatchaIsSpent.CharactersStateSystem;

namespace MatchaIsSpent.Characters.AbilitySystem
{
    /// <summary>
    /// This interface is used to create a new ability.
    /// </summary>
    public interface IAbility
    {
        /// <summary>
        /// Run the ability.
        /// <paramref name="controller"/>: The player controller.
        /// <paramref name="deltaTime"/>: The time since the last frame.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="deltaTime"></param>
        void Run(PlayerController controller, float deltaTime);
    }
}
