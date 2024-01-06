using UnityEngine;

namespace MatchaIsSpent.CharactersStateSystem
{
    /// <summary>
    /// This class is used to create a new state machine.
    /// </summary>
    public class StateMachine : MonoBehaviour
    {
        /// <summary>
        /// The current state.
        /// </summary>
        private State currentState;

        /// <summary>
        /// Set the current state.
        /// <paramref name="newState"/>: The new state.
        /// </summary>
        /// <param name="newState"></param>
        public void SetState(State newState)
        {
            currentState?.OnExit();
            currentState = newState;
            currentState?.OnEnter();
        }

        private void Update()
        {
            currentState?.OnUpdate(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            currentState?.OnFixedUpdate(Time.fixedDeltaTime);
        }
    }
}
