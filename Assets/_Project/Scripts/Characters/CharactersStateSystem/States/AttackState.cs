
namespace MatchaIsSpent.CharactersStateSystem
{
    /// <summary>
    /// This class is used to create a attack state for the player.
    /// </summary>
    public class AttackState : CharacterMovementStatePattern, ILand
    {
        private float localDeltaTime = 0;
        private float attackTime = 0.1f;

        /// <summary>
        /// Create a new attack state.
        /// <paramref name="playerController"/>: The player controller.
        /// </summary>
        /// <param name="playerController"></param>
        public AttackState(PlayerController playerController) : base(playerController)
        {
        }

        public override void OnEnter()
        {

        }

        public override void OnExit()
        {

        }

        public override void OnUpdate(float deltaTime)
        {
            localDeltaTime = deltaTime;
            attackTime -= deltaTime;
            if (attackTime <= 0)
            {
                Land();
            }
        }

        public override void OnFixedUpdate(float fixedDeltaTime)
        {

        }

        /// <summary>
        /// Land the player.
        /// Switch the player state to GroundedState.
        /// </summary>
        public void Land()
        {
            playerController.SetState(new GroundedState(playerController));
        }
    }
}

