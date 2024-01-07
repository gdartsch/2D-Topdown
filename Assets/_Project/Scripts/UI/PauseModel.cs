using System.Collections;
using UnityEngine;

namespace MatchaIsSpent.UI
{
    /// <summary>
    /// Handles the game logic of the pause menu.
    /// </summary>
    public class PauseModel : MonoBehaviour
    {
        [Tooltip("The speed at which the pause menu fades in and out.")]
        [field: SerializeField] public float FadeSpeed { get; private set; } = 2;
        [Tooltip("The sound that plays when the player opens or closes the pause menu.")]
        [field: SerializeField] public AudioClip OpenCloseSound { get; private set; }
        [Tooltip("The audisource that plays the open and close sound.")]
        [field: SerializeField] public AudioSource AudioSource { get; private set; }

        private void Awake()
        {
            PausePresenter.OnPause += PauseGame;
        }

        /// <summary>
        /// This method is called when the game is paused or unpaused.
        /// <paramref name="isPaused"/> is true if the game is paused, false if it is unpaused.
        /// </summary>
        /// <param name="isPaused"></param>
        private void PauseGame(bool isPaused)
        {
            Cursor.visible = isPaused;
            Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
            AudioSource.PlayOneShot(OpenCloseSound);
        }

        /// <summary>
        /// This method opens the secondary panel.
        /// <paramref name="secondaryPanel"/>: The secondary panel to open.
        /// </summary>
        /// <param name="secondaryPanel"></param>
        public void OpenSecondaryPanel(CanvasGroup secondaryPanel)
        {
            StartCoroutine(OpenSecondaryPanelCoroutine(secondaryPanel));
        }

        /// <summary>
        /// Delayed method to open the secondary panel.
        /// <paramref name="secondaryPanel"/>: The secondary panel to open.
        /// </summary>
        /// <param name="secondaryPanel"></param>
        /// <returns></returns>
        private IEnumerator OpenSecondaryPanelCoroutine(CanvasGroup secondaryPanel)
        {
            if (!secondaryPanel.interactable)
            {
                while (secondaryPanel.alpha < 1)
                {
                    secondaryPanel.alpha += Time.deltaTime * FadeSpeed;

                    yield return null;
                }
                secondaryPanel.interactable = true;
                secondaryPanel.blocksRaycasts = true;
            }
            else
            {
                while (secondaryPanel.alpha > 0)
                {
                    secondaryPanel.alpha -= Time.deltaTime * FadeSpeed;

                    yield return null;
                }
                secondaryPanel.interactable = false;
                secondaryPanel.blocksRaycasts = false;
            }
        }
    }
}
