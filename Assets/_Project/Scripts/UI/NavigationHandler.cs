using UnityEngine;
using UnityEngine.UI;

namespace MatchaIsSpent.UI
{
    /// <summary>
    /// Handles navigation between UI elements.
    /// </summary>
    public class NavigationHandler : MonoBehaviour
    {
        [Tooltip("The default button to select when the menu is opened.")]
        [field: SerializeField] public Button DefaultButton { get; private set; }

        /// <summary>
        /// Selects the default button.
        /// </summary>
        public void SelectDefaultButton()
        {
            DefaultButton.Select();
        }

        /// <summary>
        /// Highlights the given button.
        /// <paramref name="button"/>: The button to highlight.
        /// </summary>
        /// <param name="button"></param>
        public void HighlightButton(Button button)
        {
            button.Select();
        }
    }
}
