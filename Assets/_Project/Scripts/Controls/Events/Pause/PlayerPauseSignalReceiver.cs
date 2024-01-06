using MatchaIsSpent.Characters.AbilitySystem;
using MatchaIsSpent.CharactersStateSystem;
using UnityEngine;

namespace MatchaIsSpent.Controls
{
    /// <summary>
    /// This class is used to pause the player's movement and abilities when the game is paused.
    /// </summary>
    public class PlayerPauseSignalReceiver : PauseSignalReceiver
    {
        [Tooltip("The input reader for the player.")]
        [field: SerializeField] public InputReader InputReader { get; private set; }
        [Tooltip("The player's movement controller.")]
        [field: SerializeField] public PlayerController PlayerMovement { get; private set; }
        [Tooltip("The player's ability runner.")]
        [field: SerializeField] public AbilityRunner AbilityRunner { get; private set; }

        /// <summary>
        /// Called when the game is paused or unpaused.
        /// Pauses or unpauses the player's movement and abilities.
        /// <paramref name="isPaused"/> is true if the game is paused, false if it is unpaused.
        /// </summary>
        /// <param name="isPaused"></param>
        protected override void OnPause(bool isPaused)
        {
            base.OnPause(isPaused);

            if (isPaused)
                InputReader.playerControls.Player.Disable();
            else
                InputReader.playerControls.Player.Enable();
            InputReader.enabled = !isPaused;
            PlayerMovement.enabled = !isPaused;
            AbilityRunner.enabled = !isPaused;
        }
    }
}
