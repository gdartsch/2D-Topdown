using MatchaIsSpent.UI;
using UnityEngine;

namespace MatchaIsSpent.Controls
{
    /// <summary>
    /// This class is used to receive pause signals from the PauseHandler.
    /// </summary>
    public abstract class PauseSignalReceiver : MonoBehaviour
    {
        [Tooltip("The animator to pause when the game is paused.")]
        [field: SerializeField] public Animator animator { get; private set; }

        protected void Awake()
        {
            PausePresenter.OnPause += OnPause;
        }

        /// <summary>
        /// Called when the game is paused or unpaused.
        /// <paramref name="isPaused"/> is true if the game is paused, false if it is unpaused.
        /// </summary>
        /// <param name="isPaused"></param>
        protected virtual void OnPause(bool isPaused)
        {
            if (animator)
                animator.enabled = !isPaused;
        }

        protected void OnDestroy()
        {
            PausePresenter.OnPause -= OnPause;
        }
    }
}
