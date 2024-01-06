using System;
using System.Collections;
using MatchaIsSpent.CharactersStateSystem;
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
            controller.GetComponent<AbilityRunner>().StartCoroutine(LaunchHook(controller));
        }

        /// <summary>
        /// Launch the hook.
        /// <paramref name="playerController"/>: The player controller.
        /// </summary>
        /// <param name="playerController"></param>
        private IEnumerator LaunchHook(PlayerController playerController)
        {
            playerController.AbilitiesAudioSource.PlayOneShot(HookSound);
            playerController.Hook.SetActive(true);
            playerController.Hook.transform.position = playerController.Hand.position;

            Vector3 targetPosition = playerController.Weapon.transform.position + (Vector3)(playerController.InputReader.MoveInput * HookDistance);

            while (Vector3.Distance(playerController.Hook.transform.position, targetPosition) > 0.1f)
            {
                playerController.Hook.transform.position = Vector3.Lerp(playerController.Hook.transform.position, targetPosition, 0.5f);

                yield return null;
            }

            yield return new WaitForSeconds(0.5f);

            while (Vector3.Distance(playerController.Hook.transform.position, playerController.Hand.position) > 0.1f)
            {
                playerController.Hook.transform.position = Vector3.Lerp(playerController.Hook.transform.position, playerController.Hand.position, 0.5f);

                yield return null;
            }

            if (playerController.Hook.transform.childCount > 0)
            {
                Debug.Log(playerController.Hook.transform.childCount);
                playerController.Hook.transform.GetChild(0).GetComponent<IHookable>()?.Unhook(playerController.Hook.transform);
            }
            // Clean up after the hook action (e.g., deactivate hook)
            playerController.Hook.SetActive(false);

        }
    }
}
