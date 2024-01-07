using MatchaIsSpent.BehaviourTree;
using UnityEngine;

namespace MatchaIsSpent.AI
{
    /// <summary>
    /// This class is responsible for checking if the player is in attack range.
    /// </summary>
    public class CheckPlayerInAttackRange : Node
    {
        /// <summary>
        /// The transform of the enemy.
        /// </summary>
        private Transform transform;
        /// <summary>
        /// The animator of the enemy.
        /// </summary>
        private Animator animator;
        /// <summary>
        /// The attack range of the enemy.
        /// </summary>
        private float attackRange = 1f;

        /// <summary>
        /// Sets up the check player in attack range node.
        /// <paramref name="transform"/> The transform of the enemy.
        /// <paramref name="attackRange"/> The attack range of the enemy.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="attackRange"></param>
        public CheckPlayerInAttackRange(Transform transform, float attackRange)
        {
            this.transform = transform;
            animator = transform.GetComponent<Animator>();
            this.attackRange = attackRange;
        }

        override public NodeState Evaluate()
        {
            object target = GetData("target");

            if (target == null)
            {
                state = NodeState.FAILURE;
                return state;
            }

            Transform targetTransform = (Transform)target;

            if (Vector2.Distance(transform.position, targetTransform.position) <= attackRange)
            {
                animator.SetFloat("Speed", 0f);

                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}
