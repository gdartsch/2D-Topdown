using MatchaIsSpent.CharactersStateSystem;
using MatchaIsSpent.WorldGeneration;
using UnityEngine;

namespace MatchaIsSpent.Characters.AbilitySystem
{
    /// <summary>
    /// This class is used to create a Teleport ability.
    /// </summary>
    [CreateAssetMenu(fileName = "TeleportAbility", menuName = "MatchaIsSpent/AbilitySystem/Abilities/TeleportAbility")]
    public class TeleportAbility : BaseAbilitySO
    {
        [Tooltip("The name of the ability.")]
        [SerializeField] private string abilityName;
        [Tooltip("The distance to teleport when teleporting one tile.")]
        [field: SerializeField] public float TeleportingOneTileDistance { get; private set; } = 2f;
        [Tooltip("The distance to teleport when teleporting two tiles.")]
        [field: SerializeField] public float TeleportingTwoTilesDistance { get; private set; } = 3f;
        [Tooltip("The sound to be played when teleporting.")]
        [field: SerializeField] public AudioClip TeleportingSound { get; private set; }

        /// <summary>
        /// Run the ability.
        /// <paramref name="controller"/>: The player controller.
        /// <paramref name="deltaTime"/>: The time since the last frame.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="deltaTime"></param>
        public override void Run(PlayerController controller, float deltaTime)
        {
            Teleport(controller);
        }

        /// <summary>
        /// Teleport the player.
        /// </summary>
        /// <param name="playerController"></param>
        private void Teleport(PlayerController playerController)
        {
            Vector2 facingPlayerDirection = playerController.InputReader.MoveInput;
            var tileType = TilesInformationManager.Instance.GetTileData(playerController.WorldPositionGetter.position);
            var nextTileType = TilesInformationManager.Instance.GetTileData(playerController.WorldPositionGetter.position + (Vector3)facingPlayerDirection);

            if (tileType == null || nextTileType == null)
            {
                playerController.SetState(new GroundedState(playerController));
                return;
            }


            if ((tileType.Contains(TileType.Floor) || tileType.Contains(TileType.Grass) || tileType.Contains(TileType.CliffEdge)) &&
                nextTileType.Contains(TileType.CliffEdge))
            {
                playerController.transform.position =
                    playerController.WorldPositionGetter.position + (Vector3)facingPlayerDirection * TeleportingOneTileDistance;
                playerController.AbilitiesAudioSource.PlayOneShot(TeleportingSound);
            }
            else if ((tileType.Contains(TileType.Floor) || tileType.Contains(TileType.Grass) || tileType.Contains(TileType.CliffEdge)) &&
                nextTileType.Contains(TileType.Cliff))
            {
                playerController.transform.position =
                    playerController.WorldPositionGetter.position + (Vector3)facingPlayerDirection * TeleportingTwoTilesDistance;
                playerController.AbilitiesAudioSource.PlayOneShot(TeleportingSound);
            }
            else if (tileType.Contains(TileType.CliffEdge) &&
                (nextTileType.Contains(TileType.Floor) || nextTileType.Contains(TileType.Grass)))
            {
                playerController.transform.position =
                    playerController.WorldPositionGetter.position + (Vector3)facingPlayerDirection * TeleportingOneTileDistance;
                playerController.AbilitiesAudioSource.PlayOneShot(TeleportingSound);
            }

        }
    }
}
