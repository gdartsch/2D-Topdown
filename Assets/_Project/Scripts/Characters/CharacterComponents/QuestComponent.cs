using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestComponent : MonoBehaviour
{
    [SerializeField] private List<Quest> quests = new List<Quest>();

    public List<Quest> Quests { get => quests; set => quests = value; }

    private void Start()
    {
        foreach (var quest in Quests)
        {
            quest.StartQuest();
        }
    }

    public void AddQuest(Quest quest)
    {
        quest.StartQuest();
        Quests.Add(quest);
    }

    public void RemoveQuest(Quest quest)
    {
        Quests.Remove(quest);
    }
}
