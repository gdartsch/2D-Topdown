using System;
using System.Collections.Generic;
using UnityEngine;

namespace MatchaIsSpent.QuestSystem
{
    /// <summary>
    /// Quests are a collection of missions that must be completed in order to complete the quest.
    /// </summary>
    [CreateAssetMenu(fileName = "New Quest", menuName = "MatchaIsSpent/Quest System/Quest")]
    public class Quest : ScriptableObject
    {
        [Header("Quest Information")]
        [Tooltip("The name of the quest.")]
        [SerializeField] private string questName;
        [Tooltip("The description of the quest.")]
        [SerializeField] private string questDescription;
        [Tooltip("The list of missions that must be completed in order to complete the quest.")]
        [SerializeField] private List<Mission> missions = new List<Mission>();
        /// <summary>
        /// Determines if the quest is complete.
        /// </summary>
        private bool isComplete;

        /// <summary>
        /// Event that is invoked when a quest is started.
        /// </summary>
        public static event Action<Quest, List<Mission>> OnQuestStarted;
        /// <summary>
        /// Event that is invoked when a quest is completed.
        /// </summary>
        public static event Action<Quest> OnQuestCompleted;
        /// <summary>
        /// Event that is invoked when a mission is completed.
        /// </summary>
        public static event Action<Mission> OnMissionCompleted;

        /// <summary>
        /// The name of the quest.
        /// </summary>
        public string QuestName { get => questName; set => questName = value; }
        /// <summary>
        /// The description of the quest.
        /// </summary>
        public string QuestDescription { get => questDescription; set => questDescription = value; }
        /// <summary>
        /// The list of missions that must be completed in order to complete the quest.
        /// </summary>
        public List<Mission> Missions { get => missions; set => missions = value; }
        /// <summary>
        /// Determines if the quest is complete.
        /// </summary>
        public bool IsComplete { get => isComplete; set => isComplete = value; }

        /// <summary>
        /// Starts the quest.
        /// </summary>
        public void StartQuest()
        {
            foreach (Mission mission in missions)
            {
                mission.OnMissionCompleted += CompleteMission;
            }

            OnQuestStarted?.Invoke(this, missions);
        }

        /// <summary>
        /// Checks if all missions are complete.
        /// </summary>
        public void CheckQuestCompletion()
        {
            foreach (Mission mission in missions)
            {
                if (!mission.IsComplete) return;
            }

            CompleteQuest();
        }

        /// <summary>
        /// Completes the quest.
        /// </summary>
        private void CompleteQuest()
        {
            isComplete = true;
            OnQuestCompleted?.Invoke(this);
        }

        /// <summary>
        /// Adds a mission to the quest.
        /// <paramref name="mission"/> The mission to add.
        /// </summary>
        /// <param name="mission"></param>
        public void AddMission(Mission mission)
        {
            missions.Add(mission);
        }

        /// <summary>
        /// Removes a mission from the quest.
        /// <paramref name="mission"/> The mission to remove.
        /// </summary>
        /// <param name="mission"></param>
        public void RemoveMission(Mission mission)
        {
            missions.Remove(mission);
        }

        /// <summary>
        /// Completes a mission.
        /// <paramref name="mission"/> The mission to complete.
        /// </summary>
        /// <param name="mission"></param>
        public void CompleteMission(Mission mission)
        {
            mission.IsComplete = true;
            OnMissionCompleted?.Invoke(mission);
            CheckQuestCompletion();
        }
    }
}
