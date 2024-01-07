using MatchaIsSpent.Characters.AbilitySystem;
using UnityEngine;

namespace MatchaIsSpent.CharactersStateSystem
{
    /// <summary>
    /// This class is used to control the player.
    /// </summary>
    public class PlayerController : StateMachine
    {
        [field: Header("References")]
        [Tooltip("The animator for the player.")]
        [field: SerializeField] public Animator Animator { get; private set; }
        [Tooltip("The input reader for the player.")]
        [field: SerializeField] public InputReader InputReader { get; private set; }
        [Tooltip("The player's Rigidbody.")]
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
        [Tooltip("The player's sprite renderer.")]
        [field: SerializeField] public SpriteRenderer Renderer { get; private set; }
        [Tooltip("Helper to get the exact tile position of the player.")]
        [field: SerializeField] public Transform WorldPositionGetter { get; private set; }
        [Tooltip("The ability runner for the player.")]
        [field: SerializeField] public AbilityRunner AbilityRunner { get; private set; }
        [Tooltip("The player's Abilities AudioSource.")]
        [field: SerializeField] public AudioSource AbilitiesAudioSource { get; private set; }
        [Tooltip("The player's Weapon.")]
        [field: SerializeField] public GameObject Weapon { get; private set; }
        [Tooltip("The player's Hook.")]
        [field: SerializeField] public GameObject Hook { get; private set; }
        [Tooltip("The player's hand.")]
        [field: SerializeField] public Transform Hand { get; private set; }
        [Tooltip("The player's ManaSystem.")]
        [field: SerializeField] public ManaSystem ManaSystem { get; private set; }

        [field: Space(1), Header("Movement")]
        [Tooltip("The speed at which the player moves.")]
        [field: SerializeField] public float MovementSpeed { get; private set; } = 5f;

        private void Start()
        {
            SetState(new GroundedState(this));
        }
    }
}
