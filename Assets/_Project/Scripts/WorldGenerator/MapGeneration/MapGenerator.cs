using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace MatchaIsSpent.WorldGeneration
{
    /// <summary>
    /// This class is responsible for generating the map.
    /// </summary>
    public class MapGenerator : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Input action that generates the map")]
        [SerializeField] private InputActionReference generate = null;
        [Tooltip("Settings for map generation")]
        [SerializeField] private WorldGenerationSettings worldGenerationSettings = null;



        [Header("Events")]
        [Tooltip("Event invoked when map generation is finished")]
        public UnityEvent OnFinishedGenerationUnityEvent;

        private void Awake()
        {
            EnableInputs();
        }

        /// <summary>
        /// Enables inputs
        /// </summary>
        private void EnableInputs()
        {
            if (generate.action.actionMap != null)
            {
                generate.action.actionMap.Enable();
            }
            generate.action.performed += Generate;
        }

        /// <summary>
        /// Generates the map in runtime
        /// <paramref name="obj"/> The input action context
        /// </summary>
        /// <param name="obj"></param> The input action context
        private void Generate(InputAction.CallbackContext obj)
        {
            Generate();
        }

        /// <summary>
        /// Generates the map both in runtime and in editor
        /// </summary>
        public void Generate()
        {
            Utils.ClearConsole();

            ClearPreviousMapGeneration();

            GameObject[] submapContainer = new GameObject[worldGenerationSettings.Submaps];

            worldGenerationSettings.RoomGenerator.GenerateRooms(submapContainer);

            worldGenerationSettings.RoomGenerator.GenerateGrassAndIslands(submapContainer);

            OnFinishedGenerationUnityEvent?.Invoke();
        }

        /// <summary>
        /// Clears the previous map generation
        /// </summary>
        private void ClearPreviousMapGeneration()
        {
            worldGenerationSettings.MapData.Reset();

            worldGenerationSettings.TilesDataManager.Tilemaps.Clear();

            for (int i = worldGenerationSettings.WorldMap.transform.childCount - 1; i >= 0; i--)
                DestroyImmediate(worldGenerationSettings.WorldMap.transform.GetChild(i).gameObject);

            for (int i = worldGenerationSettings.GrassMap.transform.childCount - 1; i >= 0; i--)
                DestroyImmediate(worldGenerationSettings.GrassMap.transform.GetChild(i).gameObject);

            for (int i = worldGenerationSettings.GrassOverMap.transform.childCount - 1; i >= 0; i--)
                DestroyImmediate(worldGenerationSettings.GrassOverMap.transform.GetChild(i).gameObject);
        }

        private void OnDestroy()
        {
            DisableInputs();
        }

        /// <summary>
        /// Disables inputs
        /// </summary>
        private void DisableInputs()
        {
            if (generate.action.actionMap != null)
                generate.action.actionMap.Disable();

            generate.action.performed -= Generate;
        }
    }
}
