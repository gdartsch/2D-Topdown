using MatchaIsSpent.BehaviourTree;
using MatchaIsSpent.StatSystem;
using UnityEngine;

namespace MatchaIsSpent.AI
{
    /// <summary>
    /// This class is responsible for the enemy attack.
    /// </summary>
    public class TaskAttack : Node
    {
        /// <summary>
        /// The last target.
        /// </summary>
        private Transform lastTarget;
        /// <summary>
        /// The health system of the target.
        /// </summary>
        private HealthSystem healthSystem;
        /// <summary>
        /// The animator of the enemy.
        /// </summary>
        private Animator animator;
        /// <summary>
        /// The attack time.
        /// </summary>
        private float attackTime = 1f;
        /// <summary>
        /// The attack counter.
        /// </summary>
        private float attackCounter = 0f;

        /// <summary>
        /// Sets up the attack node.
        /// <paramref name="transform"/> The transform of the enemy.
        /// </summary>
        /// <param name="transform"></param>
        public TaskAttack(Transform transform)
        {
            animator = transform.GetComponent<Animator>();
        }

        override public NodeState Evaluate()
        {
            Transform target = (Transform)GetData("target");

            if (target != lastTarget)
            {
                healthSystem = target.GetComponent<HealthSystem>();
                lastTarget = target;
            }

            attackCounter += Time.deltaTime;
            if (attackCounter >= attackTime)
            {
                bool isDead = healthSystem.IsDead();

                if (!isDead)
                {
                    animator.Play("EnemyAttack");
                    attackCounter = 0f;
                }
                else
                {
                    ClearData("target");
                    animator.SetFloat("Speed", 1f);
                }

            }

            state = NodeState.RUNNING;
            return state;
        }
    }
}