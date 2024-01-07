using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

namespace MatchaIsSpent.Multiplayer
{
    /// <summary>
    /// This class is used to create a UDP client.
    /// Also manages everything related to multiplayer.
    /// This is one of the last classes I created for this project.
    /// I just wanted to try out multiplayer on a custom server for you to see that I can do it.
    /// </summary>
    public class UDPClient : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("The name of the player. To enforce uniqueness, it will be added to it a random number.")]
        [SerializeField] private string playerName = "Name";
        [Tooltip("The UDP client.")]
        [SerializeField] private UdpClient udpClient;
        [Tooltip("The server endpoint.")]
        [SerializeField] private IPEndPoint serverEndpoint;
        [Tooltip("The server IP.")]
        [SerializeField] private string serverIP = "127.0.0.1";
        [Tooltip("The server port.")]
        [SerializeField] private int serverPort = 5500;
        [Tooltip("The player prefab.")]
        [SerializeField] private GameObject playerPrefab;
        [Tooltip("The other players prefab.")]
        [SerializeField] private GameObject otherPlayersPrefab;
        [Tooltip("The parent transform of the players.")]
        [SerializeField] private Transform playerParent;
        [Tooltip("The virtual camera to follow the player.")]
        [SerializeField] private CinemachineVirtualCamera vCamera;
        [Tooltip("The spawn point.")]
        [SerializeField] private Transform spawnPoint;

        /// <summary>
        /// The player transform.
        /// </summary>
        private Transform playerTransform;

        /// <summary>
        /// If true, the player is not spawned to avoid spawning multiple players.
        /// </summary>
        private bool isSpawned = false;

        /// <summary>
        /// The dictionary of the remote players. It's a little lazy, but to just avoid using any more logic to the remote players,
        /// I just used the players transforms and handled the animations in this class based on positions.
        /// </summary>
        private Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();

        /// <summary>
        /// The dictionary of the previous positions of the remote players.
        /// Again, it's a little lazy, but to just avoid using any more logic to the remote players.
        /// I can do better than this, but I just wanted to show you that I can do it.
        /// </summary>
        private Dictionary<string, Vector3> previousPositions = new Dictionary<string, Vector3>();

        private void Start()
        {
            Connect();
        }

        /// <summary>
        /// Connect to the server.
        /// </summary>
        private void Connect()
        {
            playerName += UnityEngine.Random.Range(0, 1000).ToString();

            try
            {
                udpClient = new UdpClient();
                serverEndpoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);

                udpClient.BeginReceive(new AsyncCallback(ReceiveCallback), null);

                SendToServer($"Connect|{playerName}|{transform.position.x},{transform.position.y},{transform.position.z}");
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }

        /// <summary>
        /// Send a message to the server.
        /// <paramref name="message"/>: The message to send.
        /// </summary>
        /// <param name="message"></param>
        private void SendToServer(string message)
        {
            try
            {
                byte[] data = Encoding.ASCII.GetBytes(message);
                udpClient.Send(data, data.Length, serverEndpoint);
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

        /// <summary>
        /// Receive a message from the server.
        /// <paramref name="result"/>: The result of the callback.
        /// </summary>
        /// <param name="result"></param>
        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                byte[] receivedBytes = udpClient.EndReceive(result, ref remoteEP);
                string receivedMessage = Encoding.ASCII.GetString(receivedBytes);

                MainThreadDispatcher.Enqueue(() =>
                {
                    SpawnPlayers(receivedMessage);
                    GetOtherPlayersPosition(receivedMessage);
                    DisconnectPlayer(receivedMessage);
                });

                udpClient.BeginReceive(new AsyncCallback(ReceiveCallback), null);
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

        /// <summary>
        /// Spawn the players.
        /// <paramref name="receivedMessage"/>: The message received from the server.
        /// </summary>
        /// <param name="receivedMessage"></param>
        private void SpawnPlayers(string receivedMessage)
        {
            string[] splitData = null;
            string[] spawnData = null;

            if (receivedMessage.Contains(";"))
            {
                splitData = receivedMessage.Split(';');

                spawnData = new string[splitData.Length];
                for (int i = 0; i < splitData.Length; i++)
                {
                    spawnData[i] = splitData[i].Split('|')[1];


                }

                for (int i = 0; i < spawnData.Length; i++)
                {
                    Debug.Log(spawnData[i]);
                    if (spawnData[i] == playerName)
                    {
                        SpawnLocalPlayer(spawnData);
                    }
                    else if (players.ContainsKey(spawnData[i]) == false)
                    {
                        SpawnRemotePlayer(spawnData, i);
                    }
                }
            }
            else
            {
                spawnData = receivedMessage.Split('|');
            }

            if (spawnData[0] == "Spawn")
            {
                if (spawnData[1] == playerName)
                {
                    SpawnLocalPlayer(spawnData);
                }
            }

            if (receivedMessage.Contains("NewPlayer"))
            {
                SpawnRemotePlayer(spawnData, 1);
            }
        }

        /// <summary>
        /// Spawn a remote player.
        /// <paramref name="spawnData"/>: The data of the player to spawn.
        /// <paramref name="i"/>: The index of the player to spawn.
        /// </summary>
        /// <param name="spawnData"></param>
        /// <param name="i"></param>
        private void SpawnRemotePlayer(string[] spawnData, int i)
        {
            GameObject player = Instantiate(otherPlayersPrefab);
            player.transform.parent = playerParent;

            player.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z);

            player.name = spawnData[i];

            players.Add(player.name, player);
        }

        /// <summary>
        /// Spawn the local player.
        /// <paramref name="spawnData"/>: The data of the player to spawn.
        /// </summary>
        /// <param name="spawnData"></param>
        private void SpawnLocalPlayer(string[] spawnData)
        {
            if (isSpawned) return;

            isSpawned = true;

            GameObject player = Instantiate(playerPrefab);
            player.transform.parent = playerParent;
            player.name = spawnData[1];
            player.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z);
            vCamera.Follow = player.transform;
            vCamera.LookAt = player.transform;

            playerTransform = player.transform;
        }

        /// <summary>
        /// Update the remote player animation.
        /// <paramref name="playerName"/>: The name of the player.
        /// <paramref name="currentPosition"/>: The current position of the player.
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="currentPosition"></param>
        private void UpdateRemotePlayerAnimation(string playerName, Vector3 currentPosition)
        {
            if (previousPositions.ContainsKey(playerName))
            {
                Vector3 previousPosition = previousPositions[playerName];

                Vector3 moveDirection = currentPosition - previousPosition;

                Animator remotePlayerAnimator = players[playerName].GetComponent<Animator>();

                players[playerName].GetComponent<SpriteRenderer>().flipX = moveDirection.x < 0;
                remotePlayerAnimator.SetFloat("Speed", moveDirection.magnitude);
                remotePlayerAnimator.SetFloat("AnimationMoveX", moveDirection.x);
                remotePlayerAnimator.SetFloat("AnimationMoveY", moveDirection.y);
            }
            else
            {
                previousPositions.Add(playerName, currentPosition);
            }

            previousPositions[playerName] = currentPosition;
        }

        /// <summary>
        /// Handle the disconnection of a player.
        /// <paramref name="receivedMessage"/>: The message received from the server.
        /// </summary>
        /// <param name="receivedMessage"></param>
        private void DisconnectPlayer(string receivedMessage)
        {
            string[] splitDataBody = receivedMessage.Split('|');
            if (splitDataBody[0] == "Disconnect")
            {
                foreach (GameObject player in players.Values)
                {
                    if (player.name == splitDataBody[1])
                    {
                        players.Remove(player.name);
                        Destroy(player);
                    }
                }
            }
        }

        /// <summary>
        /// Send the trnasform every update to the server.
        /// This is not optimal, but again I just wanted to show you that I can do it.
        /// <paramref name="position"/>: The position of the player.
        /// </summary>
        /// <param name="position"></param>
        private void SendUpdateToServer(Vector3 position)
        {
            string message = $"Update|{playerName}|{position.x},{position.y},{position.z}";
            SendToServer(message);
        }

        private void Update()
        {
            if (playerTransform == null) return;
            Vector3 playerPosition = playerTransform.position;
            SendUpdateToServer(playerPosition);
        }

        /// <summary>
        /// Get the position of the other players.
        /// <paramref name="receivedMessage"/>: The message received from the server.
        /// </summary>
        /// <param name="receivedMessage"></param>
        private void GetOtherPlayersPosition(string receivedMessage)
        {
            string[] splitData = receivedMessage.Split('|');

            if (splitData[0] == "Update")
            {
                string[] splitPosition = splitData[2].Split(',');
                Vector3 position = new Vector3(float.Parse(splitPosition[0]), float.Parse(splitPosition[1]), float.Parse(splitPosition[2]));

                foreach (GameObject player in players.Values)
                {
                    if (player.name == splitData[1])
                    {
                        player.transform.position = position;
                        UpdateRemotePlayerAnimation(splitData[1], position);
                    }
                }
            }
        }

        /// <summary>
        /// Disconnect from the server.
        /// In case we need a way to manually disconnect from the server.
        /// Normally this is called from the Unity's OnDestroy callback.
        /// </summary>
        public void Disconnect()
        {
            SendToServer($"Disconnect|{playerName}");
            StartCoroutine(DisconnectCoroutine());
        }

        /// <summary>
        /// Disconnect from the server coroutine. Used to wait a little before disconnecting.
        /// </summary>
        private IEnumerator DisconnectCoroutine()
        {
            udpClient.Close();
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(0);
        }

        private void OnDestroy()
        {
            SendToServer($"Disconnect|{playerName}");
            udpClient.Close();
        }
    }
}