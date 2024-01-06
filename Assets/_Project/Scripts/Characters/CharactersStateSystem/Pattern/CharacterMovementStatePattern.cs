
namespace MatchaIsSpent.CharactersStateSystem
{
    /// <summary>
    /// This class is used to create a new state pattern for the character.
    /// </summary>
    public abstract class CharacterMovementStatePattern : State
    {
        protected PlayerController playerController;

        public CharacterMovementStatePattern(PlayerController playerController)
        {
            this.playerController = playerController;
        }
    }
}
