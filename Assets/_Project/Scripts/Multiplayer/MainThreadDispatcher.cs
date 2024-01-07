using UnityEngine;
using System;
using System.Collections.Generic;

namespace MatchaIsSpent.Multiplayer
{
    /// <summary>
    /// This class is used to dispatch actions to the main thread.
    /// It is needed to avoid errors that Unity throws when trying to access Unity objects from a thread other than the main thread.
    /// This is due to the fact that the server callbacks are executed in a separate thread.
    /// This class is a singleton. Normally I prefer to avoid singletons, but in this case I just needed to finish the project.
    /// </summary>
    public class MainThreadDispatcher : MonoBehaviour
    {
        /// <summary>
        /// The instance of the MainThreadDispatcher.
        /// </summary>
        private static MainThreadDispatcher instance;

        /// <summary>
        /// The queue of actions to be executed.
        /// </summary>
        private Queue<Action> actionQueue = new Queue<Action>();

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            lock (actionQueue)
            {
                while (actionQueue.Count > 0)
                {
                    Action action = actionQueue.Dequeue();
                    action?.Invoke();
                }
            }
        }

        /// <summary>
        /// Enqueue an action to be executed in the main thread.
        /// <paramref name="action"/>: The action to be executed.
        /// </summary>
        /// <param name="action"></param>
        public static void Enqueue(Action action)
        {
            if (instance == null)
            {
                Debug.LogError("MainThreadDispatcher is not initialized.");
                return;
            }

            lock (instance.actionQueue)
            {
                instance.actionQueue.Enqueue(action);
            }
        }
    }
}