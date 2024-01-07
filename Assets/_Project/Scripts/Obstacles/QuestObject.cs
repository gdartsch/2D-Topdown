using UnityEngine;

namespace MatchaIsSpent.QuestSystem
{
    /// <summary>
    /// Objects that can be used to complete missions.
    /// </summary>
    public class QuestObject : MonoBehaviour
    {
        [Tooltip("The name of the mission that this object can complete.")]
        [SerializeField] private string missionName;
        /// <summary>
        /// Determines if the object has been used.
        /// </summary>
        private bool used;

        /// <summary>
        /// The name of the mission that this object can complete.
        /// </summary>
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
}
