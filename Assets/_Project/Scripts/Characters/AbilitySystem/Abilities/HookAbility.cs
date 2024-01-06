using MatchaIsSpent.CharactersStateSystem;
using MatchaIsSpent.WorldGeneration;
using UnityEngine;

namespace MatchaIsSpent.Characters.AbilitySystem
{
    /// <summary>
    /// This class is used to create a Hook ability.
    /// </summary>
    [CreateAssetMenu(fileName = "HookAbility", menuName = "MatchaIsSpent/AbilitySystem/Abilities/HookAbility")]
    public class HookAbility : BaseAbilitySO
    {
        [Tooltip("The name of the ability.")]
        [SerializeField] private string abilityName = "Hook";
        [Tooltip("The distance to teleport when teleporting two tiles.")]
        [field: SerializeField] public float HookDistance { get; private set; } = 3f;
        [Tooltip("The sound to be played when launching the hook.")]
        [field: SerializeField] public AudioClip HookSound { get; private set; }

        /// <summary>
        /// Run the ability.
        /// <paramref name="controller"/>: The player controller.
        /// <paramref name="deltaTime"/>: The time since the last frame.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="deltaTime"></param>
        public override void Run(PlayerController controller, float deltaTime)
        {
            LaunchHook(controller);
        }

        /// <summary>
        /// Launch the hook.
        /// <paramref name="playerController"/>: The player controller.
        /// </summary>
        /// <param name="playerController"></param>
        private void LaunchHook(PlayerController playerController)
        {

        }
    }
}
