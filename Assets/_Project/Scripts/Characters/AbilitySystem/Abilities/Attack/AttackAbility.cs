using MatchaIsSpent.CharactersStateSystem;
using UnityEngine;

namespace MatchaIsSpent.Characters.AbilitySystem
{
    /// <summary>
    /// This class is used to create an Attack ability.
    /// </summary>
    [CreateAssetMenu(fileName = "AttackAbility", menuName = "MatchaIsSpent/AbilitySystem/Abilities/AttackAbility")]
    public class AttackAbility : BaseAbilitySO
    {
        [Tooltip("The name of the ability.")]
        [SerializeField] private string abilityName = "Attack";
        [Tooltip("The sound to be played when attacking.")]
        [field: SerializeField] public AudioClip AttackingSound { get; private set; }

        /// <summary>
        /// Run the ability.
        /// <paramref name="controller"/>: The player controller.
        /// <paramref name="deltaTime"/>: The time since the last frame.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="deltaTime"></param>
        public override void Run(PlayerController controller, float deltaTime)
        {
            Attack(controller);
        }

        /// <summary>
        /// Attack.
        /// <paramref name="playerController"/>: The player controller.
        /// </summary>
        /// <param name="playerController"></param>
        private void Attack(PlayerController playerController)
        {
            playerController.AbilitiesAudioSource.PlayOneShot(AttackingSound);
            playerController.Animator.Play("Attack");
        }
    }
}
