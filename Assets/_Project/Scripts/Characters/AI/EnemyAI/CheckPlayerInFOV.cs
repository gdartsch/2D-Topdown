using UnityEngine;
using MatchaIsSpent.BehaviourTree;

namespace MatchaIsSpent.AI
{
    /// <summary>
    /// Checks if the player is in the FOV of the enemy.
    /// </summary>
    public class CheckPlayerInFOV : Node
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
        /// The field of view radius of the enemy.
        /// </summary>
        private float fovRadius;

        /// <summary>
        /// Sets up the check player in FOV node.
        /// <paramref name="transform"/> The transform of the enemy.
        /// <paramref name="fovRadius"/> The field of view radius of the enemy.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="fovRadius"></param>
        public CheckPlayerInFOV(Transform transform, float fovRadius)
        {
            this.transform = transform;
            animator = transform.GetComponent<Animator>();
            this.fovRadius = fovRadius;
        }

        override public NodeState Evaluate()
        {
            object target = GetData("target");
            if (target == null)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(
                                                        transform.position,
                                                        fovRadius,
                                                        LayerMask.GetMask("Player"));
                if (colliders.Length > 0)
                {
                    parent.parent.SetData("target", colliders[0].transform);
                    animator.SetFloat("Speed", 1f);
                    state = NodeState.SUCCESS;
                    return state;
                }
                else
                {
                    state = NodeState.FAILURE;
                    return state;
                }
            }

            state = NodeState.SUCCESS;
            return state;
        }
    }
}
