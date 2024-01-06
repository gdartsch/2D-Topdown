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
            playerController.AbilitiesAudioSource.PlayOneShot(HookSound);
            playerController.Weapon.SetActive(true);
            Transform enemy = TryHook(playerController);

            if (Vector3.Distance(playerController.Weapon.transform.position, playerController.transform.position) > HookDistance)
            {
                while (Vector3.Distance(playerController.Weapon.transform.position, playerController.transform.position) < 0.1f)
                    playerController.Weapon.transform.position = Vector3.Lerp(playerController.Weapon.transform.position, playerController.transform.position, 0.5f);
            }

            ReleaseHook(playerController, enemy);
            playerController.Weapon.SetActive(false);
        }

        private static void ReleaseHook(PlayerController playerController, Transform enemyTransform)
        {
            {
                if (playerController.Weapon.transform.childCount > 0)
                {
                    playerController.Weapon.transform.GetChild(0).GetComponent<IHookable>()?.Unhook(enemyTransform);
                }
            }
        }

        /// <summary>
        /// Try to hook an object.
        /// <paramref name="playerController"/>: The player controller.
        /// </summary>
        /// <param name="playerController"></param>
        /// <returns></returns>
        private Transform TryHook(PlayerController playerController)
        {
            RaycastHit2D hit = Physics2D.Raycast(playerController.WorldPositionGetter.position, playerController.InputReader.MoveInput, HookDistance);

            if (hit.collider != null)
            {
                while (playerController.Weapon.transform.localPosition.x < HookDistance)
                {
                    playerController.Weapon.transform.position =
                                        Vector3.Lerp(playerController.Weapon.transform.position,
                                        new Vector3(playerController.Weapon.transform.position.x + HookDistance,
                                                    playerController.Weapon.transform.position.y,
                                                    playerController.Weapon.transform.position.z),
                                                    0.5f);

                    if (Vector3.Distance(playerController.Weapon.transform.position, hit.point) < 0.1f)
                    {
                        if (hit.collider.gameObject.TryGetComponent(out IHookable hookable))
                        {
                            hookable.Hook(hookable, hit.transform);
                            return hit.collider.transform;
                        }
                    }
                }
            }
            return null;
        }
    }
}
