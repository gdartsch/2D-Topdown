using System.Collections.Generic;
using UnityEngine;

namespace MatchaIsSpent.QuestSystem
{
    /// <summary>
    /// The quest component of the player.
    /// </summary>
    public class QuestComponent : MonoBehaviour
    {
        [Tooltip("The quests of the player.")]
        [SerializeField] private List<Quest> quests = new List<Quest>();

        /// <summary>
        /// The quests of the player.
        /// </summary>
        public List<Quest> Quests { get => quests; set => quests = value; }

        private void Start()
        {
            foreach (var quest in Quests)
            {
                quest.StartQuest();
            }
        }

        /// <summary>
        /// Adds a quest to the player.
        /// <paramref name="quest"/> The quest to add.
        /// </summary>
        /// <param name="quest"></param>
        public void AddQuest(Quest quest)
        {
            quest.StartQuest();
            Quests.Add(quest);
        }

        /// <summary>
        /// Removes a quest from the player.
        /// <paramref name="quest"/> The quest to remove.
        /// </summary>
        /// <param name="quest"></param>
        public void RemoveQuest(Quest quest)
        {
            Quests.Remove(quest);
        }
    }
}
