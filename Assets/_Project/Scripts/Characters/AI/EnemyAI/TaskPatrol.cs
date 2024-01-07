using MatchaIsSpent.BehaviourTree;
using UnityEngine;

namespace MatchaIsSpent.AI
{
    /// <summary>
    /// This class is responsible for patrolling the enemy.
    /// </summary>
    public class TaskPatrol : Node
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
        /// The waypoints of the enemy.
        /// </summary>
        private Transform[] waypoints;
        /// <summary>
        /// The current waypoint index.
        /// </summary>
        private int currentWaypointIndex = 0;
        /// <summary>
        /// The wait time.
        /// </summary>
        private float waitTime = 1f;
        /// <summary>
        /// The wait counter.
        /// </summary>
        private float waitCounter = 0f;
        /// <summary>
        /// Is the enemy waiting?
        /// </summary>
        private bool waiting = false;
        /// <summary>
        /// The speed of the enemy.
        /// </summary>
        private float speed;

        /// <summary>
        /// Sets up the patrol node.
        /// <paramref name="transform"/> The transform of the enemy.
        /// <paramref name="waypoints"/> The waypoints of the enemy.
        /// <paramref name="speed"/> The speed of the enemy.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="waypoints"></param>
        /// <param name="speed"></param>
        public TaskPatrol(Transform transform, Transform[] waypoints, float speed)
        {
            this.transform = transform;
            animator = transform.GetComponent<Animator>();
            this.waypoints = waypoints;
            this.speed = speed;
        }

        public override NodeState Evaluate()
        {
            if (waiting)
            {
                waitCounter += Time.deltaTime;
                if (waitCounter >= waitTime)
                {
                    waiting = false;
                    animator.SetFloat("Speed", waiting ? 0f : 1f);
                }
            }
            else
            {
                Transform waypoint = waypoints[currentWaypointIndex];
                if (Vector3.Distance(transform.position, waypoint.position) < 0.1f)
                {
                    transform.position = waypoint.position;
                    waitCounter = 0f;
                    waiting = true;

                    currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, waypoint.position, speed * Time.deltaTime);
                    float x = transform.position.x - waypoint.position.x;
                    float y = transform.position.y - waypoint.position.y;
                    animator.SetFloat("AnimationMoveX", x < 0 ? 1f : -1f);
                    animator.SetFloat("AnimationMoveY", y < 0 ? 1f : -1f);
                }
            }

            state = NodeState.RUNNING;
            return state;
        }
    }
}