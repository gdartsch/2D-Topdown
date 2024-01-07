using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "MatchaIsSpent/Quest System/Quest")]
public class Quest : ScriptableObject
{
    [SerializeField] private string questName;
    [SerializeField] private string questDescription;
    [SerializeField] private List<Mission> missions = new List<Mission>();
    private bool isComplete;

    public static event Action<Quest, List<Mission>> OnQuestStarted;
    public static event Action<Quest> OnQuestCompleted;
    public static event Action<Mission> OnMissionCompleted;

    public string QuestName { get => questName; set => questName = value; }
    public string QuestDescription { get => questDescription; set => questDescription = value; }
    public List<Mission> Missions { get => missions; set => missions = value; }
    public bool IsComplete { get => isComplete; set => isComplete = value; }

    public void StartQuest()
    {
        foreach (Mission mission in missions)
        {
            mission.OnMissionCompleted += CompleteMission;
        }

        OnQuestStarted?.Invoke(this, missions);
    }

    public void CheckQuestCompletion()
    {
        foreach (Mission mission in missions)
        {
            if (!mission.IsComplete) return;
        }

        CompleteQuest();
    }

    private void CompleteQuest()
    {
        isComplete = true;
        OnQuestCompleted?.Invoke(this);
    }

    public void AddMission(Mission mission)
    {
        missions.Add(mission);
    }

    public void RemoveMission(Mission mission)
    {
        missions.Remove(mission);
    }

    public void CompleteMission(Mission mission)
    {
        mission.IsComplete = true;
        OnMissionCompleted?.Invoke(mission);
        CheckQuestCompletion();
    }
}
