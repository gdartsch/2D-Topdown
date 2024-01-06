
namespace MatchaIsSpent.CharactersStateSystem
{
    /// <summary>
    /// This class is used to create a grounded state for the player.
    /// </summary>
    public class GroundedState : CharacterMovementStatePattern, IHook, IAttack
    {
        /// <summary>
        /// Create a new grounded state.
        /// <paramref name="playerController"/>: The player controller.
        /// </summary>
        /// <param name="playerController"></param>
        public GroundedState(PlayerController playerController) : base(playerController) { }

        /// <summary>
        /// Teleport the player.
        /// Switch the player state to TeleportingState.
        /// </summary>
        public void LaunchHook()
        {
            playerController.SetState(new TeleportingState(playerController));
        }

        public void Attack()
        {
            playerController.SetState(new AttackState(playerController));
        }

        public override void OnEnter()
        {
            playerController.InputReader.OnHookEvent += LaunchHook;
            playerController.InputReader.OnAttackEvent += Attack;
        }

        public override void OnExit()
        {
            playerController.InputReader.OnHookEvent -= LaunchHook;
            playerController.InputReader.OnAttackEvent += Attack;
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
            playerController.Renderer.flipX = playerController.InputReader.MoveInput.x < 0;
            playerController.Animator.SetFloat("Speed", playerController.InputReader.MoveInput.magnitude);
            playerController.Animator.SetFloat("AnimationMoveX", playerController.InputReader.MoveInput.x);
            playerController.Animator.SetFloat("AnimationMoveY", playerController.InputReader.MoveInput.y);
        }
    }
}
