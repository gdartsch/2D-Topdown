using MatchaIsSpent.CharactersStateSystem;
using UnityEngine;

namespace MatchaIsSpent.Characters.AbilitySystem
{
    /// <summary>
    /// This class is responsible for running the current ability.
    /// </summary>
    public class AbilityRunner : MonoBehaviour
    {
        [field: Header("Ability Sequences")]
        [Tooltip("The ability sequences for the player.")]
        [field: SerializeField] public SequenceComposite[] AbilitySequences { get; private set; }

        /// <summary>
        /// The current ability.
        /// </summary>
        public IAbility currentAbility { get; set; }

        /// <summary>
        /// Run the ability.
        /// <paramref name="controller"/>: The player controller.
        /// <paramref name="deltaTime"/>: The time since the last frame.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="deltaTime"></param>
        public void RunAbility(PlayerController controller, float deltaTime)
        {
            currentAbility?.Run(controller, deltaTime);
        }
    }
}