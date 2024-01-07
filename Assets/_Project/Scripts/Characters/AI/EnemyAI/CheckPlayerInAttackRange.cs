using System.Collections;
using System.Collections.Generic;
using MatchaIsSpent.BehaviourTree;
using UnityEngine;

namespace MatchaIsSpent.AI
{
    public class CheckPlayerInAttackRange : Node
    {
        private Transform transform;
        private Animator animator;
        private float attackRange = 1f;

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
