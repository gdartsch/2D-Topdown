using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUITracker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI isQuestActiveText;
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questMissions;
    [SerializeField] private TextMeshProUGUI questCompletion;
    private List<string> questMissionsList = new List<string>();
    private int missionsCompleted;

    private void OnEnable()
    {
        Quest.OnQuestCompleted += OnQuestCompleted;
        Quest.OnMissionCompleted += OnMissionCompleted;
        Quest.OnQuestStarted += OnQuestStarted;
    }

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
