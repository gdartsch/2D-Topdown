using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MatchaIsSpent.QuestSystem
{
    /// <summary>
    /// This class is responsible for updating the UI with the current quest information.
    /// </summary>
    public class QuestUITracker : MonoBehaviour
    {
        [Header("UI Elements")]
        [Tooltip("Text that displays if a quest is active or not.")]
        [SerializeField] private TextMeshProUGUI isQuestActiveText;
        [Tooltip("Text that displays the quest name.")]
        [SerializeField] private TextMeshProUGUI questName;
        [Tooltip("Text that displays the quest missions.")]
        [SerializeField] private TextMeshProUGUI questMissions;
        [Tooltip("Text that displays the quest completion.")]
        [SerializeField] private TextMeshProUGUI questCompletion;
        /// <summary>
        /// List of quest missions.
        /// </summary>
        private List<string> questMissionsList = new List<string>();
        /// <summary>
        /// Number of missions completed.
        /// </summary>
        private int missionsCompleted;

        private void OnEnable()
        {
            Quest.OnQuestCompleted += OnQuestCompleted;
            Quest.OnMissionCompleted += OnMissionCompleted;
            Quest.OnQuestStarted += OnQuestStarted;
        }

        /// <summary>
        /// Updates the UI with the current quest information.
        /// <paramref name="quest"/> The current quest.
        /// <paramref name="missions"/> The current quest missions.
        /// </summary>
        /// <param name="quest"></param>
        /// <param name="missions"></param>
        private void OnQuestStarted(Quest quest, List<Mission> missions)
        {
            try
            {
                isQuestActiveText.text = "Quest Active";
                questName.text = quest.QuestName;

                foreach (Mission mission in missions)
                {
                    questMissionsList.Add(mission.MissionName);
                }

                foreach (string mission in questMissionsList)
                {
                    questMissions.text += mission + "\n";
                }

                questCompletion.text = $"0 / {missions.Count}";
            }
            catch (Exception err)
            {
                Debug.Log(err);
            }

        }

        /// <summary>
        /// Updates the UI with the current quest information.
        /// <paramref name="quest"/> The current quest.
        /// </summary>
        /// <param name="quest"></param>
        private void OnQuestCompleted(Quest quest)
        {
            try
            {
                isQuestActiveText.text = "No Active Quest";
                questName.text = "";
                questMissions.text = "";
                questCompletion.text = "";
            }
            catch (Exception err)
            {
                Debug.Log(err);
            }
        }

        /// <summary>
        /// Updates the UI with the current quest information.
        /// <paramref name="mission"/> The current mission.
        /// </summary>
        /// <param name="mission"></param>
        private void OnMissionCompleted(Mission mission)
        {
            try
            {
                missionsCompleted++;

                if (questMissions.text.Contains(mission.MissionName))
                {
                    questMissions.text = questMissions.text.Replace(mission.MissionName, $"<s>{mission.MissionName}</s>");
                }

                questCompletion.text = $"{missionsCompleted} / {questMissionsList.Count}";
            }
            catch (Exception err)
            {
                Debug.Log(err);
            }
        }

        private void OnDisable()
        {
            Quest.OnQuestCompleted -= OnQuestCompleted;
            Quest.OnMissionCompleted -= OnMissionCompleted;
            Quest.OnQuestStarted -= OnQuestStarted;
        }
    }
}