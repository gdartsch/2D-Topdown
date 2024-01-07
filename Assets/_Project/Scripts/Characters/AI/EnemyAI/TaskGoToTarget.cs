using MatchaIsSpent.BehaviourTree;
using UnityEngine;

namespace MatchaIsSpent.AI
{
    /// <summary>
    /// This class is responsible for moving the enemy towards the target.
    /// </summary>
    public class TaskGoToTarget : Node
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
        /// The speed of the enemy.
        /// </summary>
        private float speed;

        /// <summary>
        /// Sets up the go to target node.
        /// <paramref name="transform"/> The transform of the enemy.
        /// <paramref name="speed"/> The speed of the enemy.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="speed"></param>
        public TaskGoToTarget(Transform transform, float speed)
        {
            this.transform = transform;
            animator = transform.GetComponent<Animator>();
            this.speed = speed;
        }

        override public NodeState Evaluate()
        {
            Transform target = (Transform)GetData("target");

            if (Vector2.Distance(transform.position, target.position) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                animator.SetFloat("Speed", 1f);
                float x = transform.position.x - target.position.x;
                float y = transform.position.y - target.position.y;
                animator.SetFloat("AnimationMoveX", x < 0 ? 1f : -1f);
                animator.SetFloat("AnimationMoveY", y < 0 ? 1f : -1f);
            }

            state = NodeState.RUNNING;
            return state;
        }
    }
}