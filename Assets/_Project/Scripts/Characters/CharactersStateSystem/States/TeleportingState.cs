namespace MatchaIsSpent.CharactersStateSystem
{
    /// <summary>
    /// This class is used to create a hook state for the player.
    /// </summary>
    public class TeleportingState : CharacterMovementStatePattern, ILand
    {
        private float localDeltaTime = 0;
        private float teleportTime = 0.1f;

        /// <summary>
        /// Create a new hook state.
        /// <paramref name="playerController"/>: The player controller.
        /// </summary>
        /// <param name="playerController"></param>
        public TeleportingState(PlayerController playerController) : base(playerController)
        {
        }

        public override void OnEnter()
        {
            playerController.AbilityRunner.currentAbility = playerController.AbilityRunner.AbilitySequences[0];
            playerController.AbilityRunner.currentAbility.Run(playerController, localDeltaTime);
        }

        public override void OnExit()
        {

        }

        public override void OnUpdate(float deltaTime)
        {
            localDeltaTime = deltaTime;
            teleportTime -= deltaTime;
            if (teleportTime <= 0)
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
