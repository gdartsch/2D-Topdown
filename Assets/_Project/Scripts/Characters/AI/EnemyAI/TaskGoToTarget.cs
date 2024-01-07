using System.Collections;
using System.Collections.Generic;
using MatchaIsSpent.BehaviourTree;
using UnityEngine;

namespace MatchaIsSpent.AI
{
    public class TaskGoToTarget : Node
    {
        private Transform transform;
        private Animator animator;
        private float speed;

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