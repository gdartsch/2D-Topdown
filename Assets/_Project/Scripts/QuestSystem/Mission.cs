using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mission", menuName = "MatchaIsSpent/Quest System/Mission")]
public class Mission : ScriptableObject
{
    [SerializeField] private string missionName;
    [SerializeField] private string missionDescription;
    private bool isCompleted;
    public string MissionName { get => missionName; set => missionName = value; }
    public string MissionDescription { get => missionDescription; set => missionDescription = value; }
    public bool IsComplete { get => isCompleted; set => isCompleted = value; }
    public event Action<Mission> OnMissionCompleted;

    public void CompleteMission()
    {
        isCompleted = true;
        OnMissionCompleted?.Invoke(this);
    }
}
