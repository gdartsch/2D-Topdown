using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchaIsSpent.BehaviourTree;

namespace MatchaIsSpent.AI
{
    public class CheckPlayerInFOV : Node
    {
        private Transform transform;
        private Animator animator;
        private float fovRadius;

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
