
namespace MatchaIsSpent.CharactersStateSystem
{
    /// <summary>
    /// This class is used to create a state for the player.
    /// </summary>
    public abstract class State
    {
        /// <summary>
        /// Called when the state is entered.
        /// </summary>
        public abstract void OnEnter();

        /// <summary>
        /// Called when the state is exited.
        /// </summary>
        public abstract void OnExit();

        /// <summary>
        /// Called every frame.
        /// <paramref name="deltaTime"/>: The time since the last frame.
        /// </summary>
        /// <param name="deltaTime"></param>
        public abstract void OnUpdate(float deltaTime);

        /// <summary>
        /// Called every fixed frame.
        /// <paramref name="fixedDeltaTime"/>: The time since the last fixed frame.
        /// </summary>
        /// <param name="fixedDeltaTime"></param>
        public abstract void OnFixedUpdate(float fixedDeltaTime);
    }
}
