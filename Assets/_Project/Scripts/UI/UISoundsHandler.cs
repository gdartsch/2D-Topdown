using UnityEngine;

namespace MatchaIsSpent.UI
{
    /// <summary>
    /// This class handles the sounds of the UI.
    /// </summary>
    public class UISoundsHandler : MonoBehaviour
    {
        [field: Header("Audio Clips")]
        [Tooltip("The audio clip that plays when the player hovers over a button.")]
        [field: SerializeField] public AudioClip HoverSound { get; private set; }
        [Tooltip("The audio clip that plays when the player clicks a button.")]
        [field: SerializeField] public AudioClip ClickSound { get; private set; }

        [field: Header("References")]
        [Tooltip("The audio source that plays the sounds.")]
        [field: SerializeField] public AudioSource AudioSource { get; private set; }

        private bool isPaused = false;

        private void Awake()
        {
            UISoundEffectTrigger.OnHover += OnHoverButton;
            UISoundEffectTrigger.OnClick += OnClickButton;
            PausePresenter.OnPause += SetIsPaused;
        }

        /// <summary>
        /// This method is called when the game is paused or unpaused.
        /// <paramref name="isPaused"/> is true if the game is paused, false if it is unpaused.
        /// </summary>
        /// <param name="isPaused"></param>
        public void SetIsPaused(bool isPaused)
        {
            this.isPaused = isPaused;
        }

        /// <summary>
        /// This method is called when the player hovers over a button.
        /// </summary>
        public void OnHoverButton()
        {
            if (isPaused)
                AudioSource.PlayOneShot(HoverSound);
        }

        /// <summary>
        /// This method is called when the player clicks a button.
        /// </summary>
        public void OnClickButton()
        {
            AudioSource.PlayOneShot(ClickSound);
        }

        private void OnDestroy()
        {
            UISoundEffectTrigger.OnHover -= OnHoverButton;
            UISoundEffectTrigger.OnClick -= OnClickButton;
            PausePresenter.OnPause -= SetIsPaused;
        }
    }
}
