using System;
using UnityEngine;

namespace MatchaIsSpent.QuestSystem
{
    /// <summary>
    /// Missions are a collection of tasks that must be completed in order to complete the mission.
    /// </summary>
    [CreateAssetMenu(fileName = "New Mission", menuName = "MatchaIsSpent/Quest System/Mission")]
    public class Mission : ScriptableObject
    {
        [Header("Mission Information")]
        [Tooltip("The name of the mission.")]
        [SerializeField] private string missionName;
        [Tooltip("The description of the mission.")]
        [SerializeField] private string missionDescription;
        /// <summary>
        /// Determines if the mission is complete.
        /// </summary>
        private bool isCompleted;
        /// <summary>
        /// The name of the mission.
        /// </summary>
        public string MissionName { get => missionName; set => missionName = value; }
        /// <summary>
        /// The description of the mission.
        /// </summary>
        public string MissionDescription { get => missionDescription; set => missionDescription = value; }
        /// <summary>
        /// Determines if the mission is complete.
        /// </summary>
        public bool IsComplete { get => isCompleted; set => isCompleted = value; }
        /// <summary>
        /// Event that is invoked when a mission is completed.
        /// </summary>
        public event Action<Mission> OnMissionCompleted;

        /// <summary>
        /// Completes the mission.
        /// </summary>
        public void CompleteMission()
        {
            isCompleted = true;
            OnMissionCompleted?.Invoke(this);
        }
    }
}