
namespace MatchaIsSpent.CharactersStateSystem
{
    /// <summary>
    /// This class is used to create a grounded state for the player.
    /// </summary>
    public class GroundedState : CharacterMovementStatePattern, IHook, IAttack, ITeleport
    {
        /// <summary>
        /// Create a new grounded state.
        /// <paramref name="playerController"/>: The player controller.
        /// </summary>
        /// <param name="playerController"></param>
        public GroundedState(PlayerController playerController) : base(playerController) { }

        /// <summary>
        /// Launch the player's hook.
        /// Switch the player state to HookState.
        /// </summary>
        public void LaunchHook()
        {
            playerController.SetState(new HookState(playerController));
        }

        /// <summary>
        /// Teleport the player.
        /// Switch the player state to TeleportingState.
        /// </summary>
        public void Teleport()
        {
            playerController.SetState(new TeleportingState(playerController));
        }

        /// <summary>
        /// Attack.
        /// Switch the player state to AttackState.
        /// </summary>
        public void Attack()
        {
            playerController.SetState(new AttackState(playerController));
        }

        public override void OnEnter()
        {
            playerController.InputReader.OnHookEvent += LaunchHook;
            playerController.InputReader.OnAttackEvent += Attack;
            playerController.InputReader.OnTeleportEvent += Teleport;
        }

        public override void OnExit()
        {
            playerController.InputReader.OnHookEvent -= LaunchHook;
            playerController.InputReader.OnAttackEvent -= Attack;
            playerController.InputReader.OnTeleportEvent -= Teleport;
        }

        public override void OnUpdate(float deltaTime)
        {
            Animate();
        }

        public override void OnFixedUpdate(float fixedDeltaTime)
        {
            playerController.Rigidbody2D.MovePosition(
                playerController.Rigidbody2D.position +
                playerController.InputReader.MoveInput *
                (fixedDeltaTime *
                playerController.MovementSpeed));
        }

        /// <summary>
        /// Animate the player.
        /// </summary>
        private void Animate()
        {
            playerController.Animator.SetFloat("Speed", playerController.InputReader.MoveInput.magnitude);
            playerController.Animator.SetFloat("AnimationMoveX", playerController.InputReader.MoveInput.x);
            playerController.Animator.SetFloat("AnimationMoveY", playerController.InputReader.MoveInput.y);
        }
    }
}
