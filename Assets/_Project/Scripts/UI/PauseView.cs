using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace MatchaIsSpent.UI
{
    /// <summary>
    /// Handles the view of the pause menu.
    /// </summary>
    public class PauseView : MonoBehaviour
    {
        [field: Header("References")]
        [Tooltip("The playable director that will play the animations.")]
        [field: SerializeField] public PlayableDirector Director { get; private set; }
        [Tooltip("The timeline that will be played when the game is paused.")]
        [field: SerializeField] public TimelineAsset OpenTimeline { get; private set; }
        [Tooltip("The timeline that will be played when the game is unpaused.")]
        [field: SerializeField] public TimelineAsset CloseTimeline { get; private set; }
        [Tooltip("The pause button.")]
        [field: SerializeField] public InputActionReference PauseButton { get; private set; }

        /// <summary>
        /// A flag that indicates if the game is paused or not.
        /// </summary>
        private bool isPaused = false;

        private void Awake()
        {
            PauseButton.action.performed += OnPauseButton;
        }

        /// <summary>
        /// This method is called when the pause button is pressed.
        /// It plays the animations and disables the canvas group.
        /// <paramref name="context"/>: The context of the input action.
        /// </summary>
        /// <param name="context"></param>
        private void OnPauseButton(InputAction.CallbackContext context)
        {
            isPaused = !isPaused;
            StartCoroutine(PlayAnimation());
        }

        /// <summary>
        /// This method plays the animations and disables the canvas group.
        /// </summary>
        /// <returns></returns>
        public IEnumerator PlayAnimation()
        {
            Director.Stop();
            yield return new WaitForSeconds(0.1f);

            Director.playableAsset = null;

            yield return new WaitForSeconds(0.1f);

            Director.playableAsset = isPaused ? OpenTimeline : CloseTimeline;

            yield return new WaitForSeconds(0.1f);

            Director.Play();
        }

        private void OnDestroy()
        {
            PauseButton.action.performed -= OnPauseButton;
        }
    }
}
