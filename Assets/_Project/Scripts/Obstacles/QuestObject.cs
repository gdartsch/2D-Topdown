using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    [SerializeField] private string missionName;
    private bool used;

    public string MissionName { get => missionName; set => missionName = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (used) return;

        if (collision.TryGetComponent(out QuestComponent questComponent))
        {
            questComponent.Quests.ForEach(quest =>
            {
                quest.Missions.ForEach(mission =>
                {
                    if (mission.MissionName == MissionName)
                    {
                        mission.CompleteMission();
                        used = true;
                    }
                });
            });
        }
    }
}
