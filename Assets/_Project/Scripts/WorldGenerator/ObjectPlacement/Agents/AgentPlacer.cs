using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace MatchaIsSpent.WorldGeneration
{
    /// <summary>
    /// This class is responsible for placing the agents in the world.
    /// </summary>
    public class AgentPlacer : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("The number of NPCs to be placed in each room. The index of the list corresponds to the index of the room in the map data.")]
        [SerializeField] private List<int> roomNPCsCount;
        [Tooltip("The index of the room where the player will be placed.")]
        [SerializeField] private ushort playerRoomIndex;

        [Header("References")]
        [Tooltip("The NPC prefab to be placed in the world.")]
        [SerializeField] private GameObject npcsPrefab = null;
        [Tooltip("The player prefab to be placed in the world.")]
        [SerializeField] private GameObject playerPrefab = null;
        [Tooltip("The parent transform of the NPCs.")]
        [SerializeField] private Transform npcsParent = null;
        [Tooltip("The parent transform of the player.")]
        [SerializeField] private Transform playerParent = null;
        [Tooltip("The virtual camera to follow the player.")]
        [SerializeField] private CinemachineVirtualCamera vCamera;
        [Tooltip("The map data.")]
        [SerializeField] private MapData mapData;

        [Header("Settings")]
        [Tooltip("If true, the player will be placed in the world. If false, let the Multiplayer System handle the spawning.")]
        [SerializeField] private bool placePlayer = true;

        [Header("Events")]
        [Tooltip("The event invoked when the agent placement is finished.")]
        public UnityEvent OnFinishedAgentPlacementUnityEvent;

        /// <summary>
        /// Places the agents in the world.
        /// </summary>
        public void PlaceAgents()
        {
            for (int i = npcsParent.transform.childCount - 1; i >= 0; i--)
                DestroyImmediate(npcsParent.transform.GetChild(i)?.gameObject);

            for (int i = playerParent.transform.childCount - 1; i >= 0; i--)
                DestroyImmediate(playerParent.transform.GetChild(i)?.gameObject);

            if (!mapData)
                return;

            for (short i = 0; i < mapData.Rooms.Count; i++)
            {
                Room room = mapData.Rooms[i];
                RoomGraph roomGraph = new RoomGraph(room.FloorTiles);
                HashSet<Vector2Int> roomFloor = new HashSet<Vector2Int>(room.FloorTiles);
                Dictionary<Vector2Int, Vector2Int> roomMap = roomGraph.RunBinaryPartitionSpace(roomFloor.First(), room.PropPositions);
                room.PositionsAccessibleFromPath = roomMap.Keys.OrderBy(x => Guid.NewGuid()).ToList();

                if (roomNPCsCount.Count > i)
                    PlaceNPCs(room, roomNPCsCount[i]);

                if (i == playerRoomIndex && placePlayer)
                {
                    GameObject player = Instantiate(playerPrefab);
                    player.transform.parent = playerParent;

                    player.transform.position = mapData.Rooms[i].RoomCenterPos + Vector2.one * 0.5f;
                    vCamera.Follow = player.transform;
                    vCamera.LookAt = player.transform;
                    mapData.PlayerReference = player;
                }
            }

            OnFinishedAgentPlacementUnityEvent?.Invoke();
        }

        /// <summary>
        /// Places the NPCs in the room.
        /// <paramref name="room"/> The room where the NPCs will be placed.
        /// <paramref name="nPCsCount"/> The number of NPCs to be placed.
        /// </summary>
        /// <param name="room"></param>
        /// <param name="nPCsCount"></param>
        private void PlaceNPCs(Room room, int nPCsCount)
        {
            for (short i = 0; i < nPCsCount; i++)
            {
                if (room.PositionsAccessibleFromPath.Count <= i) return;

                GameObject npc = Instantiate(npcsPrefab);
                npc.transform.SetParent(npcsParent);
                npc.transform.localPosition = room.PositionsAccessibleFromPath[i] + Vector2.one * 0.5f;
                npc.GetComponent<SpriteRenderer>().flipX = UnityEngine.Random.value > 0.5f;
                room.NPCsInTheRoom.Add(npc);
            }
        }
    }
}
