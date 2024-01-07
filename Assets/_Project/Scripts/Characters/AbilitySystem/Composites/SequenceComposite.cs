using MatchaIsSpent.CharactersStateSystem;
using UnityEngine;

namespace MatchaIsSpent.Characters.AbilitySystem
{
    /// <summary>
    /// This class is used to create a sequence of abilities.
    /// </summary>
    [CreateAssetMenu(fileName = "AbilitySequenceComposite", menuName = "MatchaIsSpent/AbilitySystem/Composites/SequenceComposite")]
    public class SequenceComposite : BaseAbilitySO
    {
        [Tooltip("The name of the abilitie's sequence.")]
        [SerializeField] protected BaseAbilitySO[] children;

        /// <summary>
        /// Run the ability.
        /// <paramref name="controller"/>: The player controller.
        /// <paramref name="deltaTime"/>: The time since the last frame.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="deltaTime"></param>
        public override void Run(PlayerController controller, float deltaTime)
        {
            foreach (IAbility child in children)
            {
                child.Run(controller, deltaTime);
            }
        }
    }
}
