using System;
using UnityEngine;
using UnityEngine.UI;

namespace MatchaIsSpent.UI
{
    /// <summary>
    /// This class is used to trigger the sound effects of the UI.
    /// </summary>
    public class UISoundEffectTrigger : MonoBehaviour
    {
        [field: Header("References")]
        [Tooltip("The button that triggers the sound effect.")]
        [field: SerializeField] public Button Button { get; private set; }

        /// <summary>
        /// Called when the player hovers over the button.
        /// </summary>
        public static event Action OnHover;
        /// <summary>
        /// Called when the player clicks the button.
        /// </summary>
        public static event Action OnClick;

        private void Awake()
        {
            Button.onClick.AddListener(OnButtonClicked);
        }

        /// <summary>
        /// This method is called when the player clicks the button.
        /// </summary>
        private void OnButtonClicked()
        {
            OnClick?.Invoke();
        }

        /// /// <summary>
        /// This method is called when the player hovers over the button.
        /// </summary>
        public void OnButtonSelect()
        {
            OnHover?.Invoke();
        }
    }
}
