
namespace MatchaIsSpent.CharactersStateSystem
{
    /// <summary>
    /// This class is used to create a grounded state for the player.
    /// </summary>
    public class GroundedState : CharacterMovementStatePattern, IHook, IMeleeAttack
    {
        /// <summary>
        /// Create a new grounded state.
        /// <paramref name="playerController"/>: The player controller.
        /// </summary>
        /// <param name="playerController"></param>
        public GroundedState(PlayerController playerController) : base(playerController) { }

        /// <summary>
        /// Launch hook.
        /// Switch the player state to Hook State.
        /// </summary>
        public void LaunchHook()
        {
            playerController.SetState(new HookState(playerController));
        }

        /// <summary>
        /// Melee attack.
        /// Switch the player state to Attack State.
        /// </summary>
        public void MeleeAttack()
        {
            playerController.SetState(new AttackState(playerController));
        }

        public override void OnEnter()
        {
            playerController.InputReader.OnHookEvent += LaunchHook;
        }

        public override void OnExit()
        {
            playerController.InputReader.OnHookEvent -= LaunchHook;
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
