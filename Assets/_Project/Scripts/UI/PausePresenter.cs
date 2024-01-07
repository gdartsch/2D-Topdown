using UnityEngine;
using UnityEngine.InputSystem;

namespace MatchaIsSpent.UI
{
    /// <summary>
    /// Handles the inputs and events when pausing the game.
    /// </summary>
    public class PausePresenter : MonoBehaviour
    {
        [Tooltip("The input action that pauses the game.")]
        [field: SerializeField] public InputActionReference PauseButton { get; private set; }

        [Tooltip("A flag that indicates if this is the main screen.")]
        [SerializeField] private bool IsMainScreen;

        /// <summary>
        /// Called when the game is paused or unpaused.
        /// <paramref name="isPaused"/> is true if the game is paused, false if it is unpaused.
        /// </summary>
        /// <param name="isPaused"></param>
        public delegate void PauseHandlerDelegate(bool isPaused);
        public static event PauseHandlerDelegate OnPause;

        private bool isPaused = false;

        private void Awake()
        {
            PauseButton.action.Enable();
            PauseButton.action.performed += OnPauseButton;
        }

        /// <summary>
        /// This method is called when the pause button is pressed.
        /// <paramref name="context"/>: The context of the input action.
        /// </summary>
        /// <param name="context"></param>
        private void OnPauseButton(InputAction.CallbackContext context)
        {
            if (IsMainScreen)
            {
                return;
            }
            isPaused = !isPaused;
            OnPause?.Invoke(isPaused);
        }

        /// <summary>
        /// This method is called when the game is paused or unpaused.
        /// </summary>
        public void FireEventAfterRegeneration()
        {
            isPaused = true;
            OnPause?.Invoke(isPaused);
        }

        private void OnDestroy()
        {
            PauseButton.action.performed -= OnPauseButton;
            PauseButton.action.Disable();
        }
    }
}