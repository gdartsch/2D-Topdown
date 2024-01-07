using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace MatchaIsSpent.WorldGeneration
{
    /// <summary>
    /// Enum to define the corner of the room where the prop will be placed.
    /// </summary>
    public enum PlacementOriginCorner
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    /// <summary>
    /// This class is used to place props in the room.
    /// </summary>
    public class PropPlacementManager : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("Chance to place a prop in a corner of the room.")]
        [SerializeField, Range(0, 1)] private float cornerPropPlacementChance = 0.7f;
        [Tooltip("List of props to place in the room.")]
        [SerializeField] private List<Prop> propsToPlace;
        [Tooltip("Maximum number of enemy spawners per room.")]
        [SerializeField] private int maxEnemySpawnersPerRoom = 4;
        [Tooltip("Number of potions per room.")]
        [SerializeField] private int potionsPerRoom = 20;

        [Space(1), Header("References")]
        [Tooltip("Map data of the room.")]
        [SerializeField] private MapData mapData;
        [Tooltip("Parent of the props.")]
        [SerializeField] private Transform parent;
        [Tooltip("Prefab of the prop.")]
        [SerializeField] private GameObject propPrefab;
        [Tooltip("Prefab of the enemy spawner.")]
        [SerializeField] private GameObject enemySpawnerPrefab;
        [Tooltip("Prefab of the key spawner.")]
        [SerializeField] private GameObject keyPrefab;
        [Tooltip("Prefab of the door.")]
        [SerializeField] private GameObject doorPrefab;
        [Tooltip("Prefab of the health potions.")]
        [SerializeField] private GameObject manaPotionsPrefab;
        [Tooltip("Prefab of the health potions.")]
        [SerializeField] private GameObject healthPotionsPrefab;

        [Space(1), Header("Events")]
        [Tooltip("Event invoked when the props are placed.")]
        public UnityEvent OnFinishedPropPlacementUnityEvent;

        /// <summary>
        /// This method is used to place the props in the room.
        /// </summary>
        public void ProcessRooms()
        {
            for (int i = parent.transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(parent.transform.GetChild(i).gameObject);
            }

            if (mapData == null)
                return;

            foreach (Room room in mapData.Rooms)
            {
                List<Prop> cornerProps = propsToPlace.Where(x => x.Corner).ToList();
                PlaceCornerProps(room, cornerProps);

                List<Prop> leftWallProps = propsToPlace
                .Where(x => x.NearWallLeft)
                .OrderByDescending(x => x.PropSize.x * x.PropSize.y)
                .ToList();

                PlaceProps(room, leftWallProps, room.NearWallsTilesLeft, PlacementOriginCorner.BottomLeft);

                List<Prop> rightWallProps = propsToPlace
                .Where(x => x.NearWallRight)
                .OrderByDescending(x => x.PropSize.x * x.PropSize.y)
                .ToList();

                PlaceProps(room, rightWallProps, room.NearWallsTilesRight, PlacementOriginCorner.TopRight);

                List<Prop> upWallProps = propsToPlace
                .Where(x => x.NearWallUp)
                .OrderByDescending(x => x.PropSize.x * x.PropSize.y)
                .ToList();

                PlaceProps(room, upWallProps, room.NearWallsTilesUp, PlacementOriginCorner.TopLeft);

                List<Prop> downWallProps = propsToPlace
                .Where(x => x.NearWallDown)
                .OrderByDescending(x => x.PropSize.x * x.PropSize.y)
                .ToList();

                PlaceProps(room, downWallProps, room.NearWallsTilesDown, PlacementOriginCorner.BottomLeft);

                List<Prop> innerProps = propsToPlace.Where(x => x.Inner).ToList();
                PlaceProps(room, innerProps, room.InnerTiles, PlacementOriginCorner.BottomLeft);
            }

            PlaceEnemySpawners();
            PlaceKey();
            PlaceDoor();
            PlacePotions();


            OnFinishedPropPlacementUnityEvent?.Invoke();
        }

        /// <summary>
        /// This method is used to place the potions in the room.
        /// </summary>
        private void PlacePotions()
        {
            for (int i = 0; i < mapData.Rooms.Count; i++)
            {
                for (int j = 0; j < potionsPerRoom; j++)
                {
                    Room room = mapData.Rooms[i];

                    if (room == null)
                        continue;

                    int enemySpawnersCount = UnityEngine.Random.Range(1, maxEnemySpawnersPerRoom + 1);

                    HashSet<Vector2Int> innerTiles = room.InnerTiles;

                    if (innerTiles.Count == 0)
                        continue;

                    Vector2Int randomTile = innerTiles.OrderBy(x => Guid.NewGuid()).First();

                    GameObject potion = Instantiate(UnityEngine.Random.value > 0.5f ? healthPotionsPrefab : manaPotionsPrefab);
                    potion.transform.SetParent(parent);
                    potion.transform.position = randomTile + Vector2.one * 0.5f;
                }

            }
        }

        /// <summary>
        /// This method is used to place the key in a random room.
        /// </summary>
        private void PlaceKey()
        {
            bool placed = false;
            for (int i = 0; i < mapData.Rooms.Count; i++)
            {
                Room room = mapData.Rooms[UnityEngine.Random.Range(0, mapData.Rooms.Count)];

                if (placed)
                    break;

                if (room == null)
                    continue;

                HashSet<Vector2Int> innerTiles = room.InnerTiles;

                if (innerTiles.Count == 0)
                    continue;

                Vector2Int randomTile = innerTiles.OrderBy(x => Guid.NewGuid()).First();

                GameObject key = Instantiate(keyPrefab);
                key.transform.SetParent(parent);
                key.transform.position = randomTile + Vector2.one * 0.5f;

                placed = true;
            }
        }

        /// <summary>
        /// This method is used to place the door in a random room.
        /// </summary>
        private void PlaceDoor()
        {
            bool placed = false;
            for (int i = 0; i < mapData.Rooms.Count; i++)
            {
                Room room = mapData.Rooms[UnityEngine.Random.Range(0, mapData.Rooms.Count)];

                if (placed)
                    break;

                if (room == null)
                    continue;

                HashSet<Vector2Int> innerTiles = room.InnerTiles;

                if (innerTiles.Count == 0)
                    continue;

                Vector2Int randomTile = innerTiles.OrderBy(x => Guid.NewGuid()).First();

                GameObject door = Instantiate(doorPrefab);
                door.transform.SetParent(parent);
                door.transform.position = randomTile + Vector2.one * 0.5f;

                placed = true;
            }
        }

        /// <summary>
        /// This method is used to place the enemy spawners in the room.
        /// </summary>
        private void PlaceEnemySpawners()
        {
            for (int i = 0; i < mapData.Rooms.Count; i++)
            {
                for (int j = 0; j < maxEnemySpawnersPerRoom; j++)
                {
                    Room room = mapData.Rooms[i];

                    if (room == null)
                        continue;

                    int enemySpawnersCount = UnityEngine.Random.Range(1, maxEnemySpawnersPerRoom + 1);

                    HashSet<Vector2Int> innerTiles = room.InnerTiles;

                    if (innerTiles.Count == 0)
                        continue;

                    Vector2Int randomTile = innerTiles.OrderBy(x => Guid.NewGuid()).First();

                    GameObject enemySpawner = Instantiate(enemySpawnerPrefab);
                    enemySpawner.transform.SetParent(parent);
                    enemySpawner.transform.position = randomTile + Vector2.one * 0.5f;
                }

            }
        }

        /// <summary>
        /// This method is used to place the props in the room.
        /// <paramref name="wallProps"/> is the list of props to place.
        /// <paramref name="nearWallsTiles"/> is the list of tiles near the wall.
        /// <paramref name="placement"/> is the corner of the room where the prop will be placed.
        /// </summary>
        /// <param name="room"></param>
        /// <param name="wallProps"></param>
        /// <param name="nearWallsTiles"></param>
        /// <param name="placement"></param>
        private void PlaceProps(Room room, List<Prop> wallProps, HashSet<Vector2Int> nearWallsTiles, PlacementOriginCorner placement)
        {
            HashSet<Vector2Int> tempPositions = new HashSet<Vector2Int>(nearWallsTiles);
            tempPositions.ExceptWith(mapData.Path);

            foreach (Prop propToPlace in wallProps)
            {
                int quantity = UnityEngine.Random.Range(propToPlace.PlacementQuantityMin, propToPlace.PlacementQuantityMax + 1);

                for (short i = 0; i < quantity; i++)
                {
                    tempPositions.ExceptWith(room.PropPositions);

                    List<Vector2Int> availablePositions = tempPositions.OrderBy(x => Guid.NewGuid()).ToList();

                    if (!TryPlacingPropBruteForce(room, propToPlace, availablePositions, placement))
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to place the props in the room.
        /// <paramref name="room"/> is the room to place the props in.
        /// <paramref name="propToPlace"/> is the prop to place.
        /// <paramref name="availablePositions"/> is the list of available positions to place the prop.
        /// <paramref name="placement"/> is the corner of the room where the prop will be placed.
        /// </summary>
        /// <param name="room"></param>
        /// <param name="propToPlace"></param>
        /// <param name="availablePositions"></param>
        /// <param name="placement"></param>
        /// <returns>
        /// True if the prop was placed.
        /// </returns>
        private bool TryPlacingPropBruteForce(Room room, Prop propToPlace, List<Vector2Int> availablePositions, PlacementOriginCorner placement)
        {
            for (short i = 0; i < availablePositions.Count; i++)
            {
                Vector2Int position = availablePositions[i];

                if (room.PropPositions.Contains(position))
                {
                    continue;
                }

                List<Vector2Int> freePositionsAround = TryToFitProp(propToPlace, availablePositions, position, placement);

                if (freePositionsAround.Count == propToPlace.PropSize.x * propToPlace.PropSize.y)
                {
                    PlacePropGameObjectAt(room, position, propToPlace);

                    foreach (Vector2Int pos in freePositionsAround)
                    {
                        room.PropPositions.Add(pos);
                    }

                    if (propToPlace.PlaceAsGroup)
                    {
                        PlaceGroupObject(room, position, propToPlace, 2);
                    }
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// This method is used to get the free positions around the prop.
        /// <paramref name="prop"/> is the prop to place.
        /// <paramref name="availablePositions"/> is the list of available positions to place the prop.
        /// <paramref name="originPosition"/> is the position of the prop.
        /// <paramref name="placement"/> is the corner of the room where the prop will be placed.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="availablePositions"></param>
        /// <param name="originPosition"></param>
        /// <param name="placement"></param>
        /// <returns>
        /// A list of free positions around the prop.
        /// </returns>
        private List<Vector2Int> TryToFitProp(Prop prop, List<Vector2Int> availablePositions, Vector2Int originPosition, PlacementOriginCorner placement)
        {
            List<Vector2Int> freePositions = new List<Vector2Int>();

            if (placement == PlacementOriginCorner.BottomLeft)
            {
                for (short xOffset = 0; xOffset < prop.PropSize.x; xOffset++)
                {
                    for (short yOffset = 0; yOffset < prop.PropSize.y; yOffset++)
                    {
                        Vector2Int positionToCheck = originPosition + new Vector2Int(xOffset, yOffset);

                        if (availablePositions.Contains(positionToCheck))
                        {
                            freePositions.Add(positionToCheck);
                        }
                    }
                }
            }
            else if (placement == PlacementOriginCorner.BottomRight)
            {
                for (int xOffset = -prop.PropSize.x + 1; xOffset <= 0; xOffset++)
                {
                    for (int yOffset = 0; yOffset < prop.PropSize.y; yOffset++)
                    {
                        Vector2Int positionToCheck = originPosition + new Vector2Int(xOffset, yOffset);

                        if (availablePositions.Contains(positionToCheck))
                        {
                            freePositions.Add(positionToCheck);
                        }
                    }
                }
            }
            else if (placement == PlacementOriginCorner.TopLeft)
            {
                for (short xOffset = 0; xOffset < prop.PropSize.x; xOffset++)
                {
                    for (int yOffset = -prop.PropSize.y + 1; yOffset <= 0; yOffset++)
                    {
                        Vector2Int positionToCheck = originPosition + new Vector2Int(xOffset, yOffset);

                        if (availablePositions.Contains(positionToCheck))
                        {
                            freePositions.Add(positionToCheck);
                        }
                    }
                }
            }
            else
            {
                for (int xOffset = -prop.PropSize.x + 1; xOffset <= 0; xOffset++)
                {
                    for (int yOffset = -prop.PropSize.y + 1; yOffset <= 0; yOffset++)
                    {
                        Vector2Int positionToCheck = originPosition + new Vector2Int(xOffset, yOffset);

                        if (availablePositions.Contains(positionToCheck))
                        {
                            freePositions.Add(positionToCheck);
                        }
                    }
                }
            }

            return freePositions;
        }

        /// <summary>
        /// This method is used to place the props in the corners of the room.
        /// <paramref name="room"/> is the room to place the props in.
        /// <paramref name="cornerProps"/> is the list of props to place.
        /// </summary>
        /// <param name="room"></param>
        /// <param name="cornerProps"></param>
        private void PlaceCornerProps(Room room, List<Prop> cornerProps)
        {
            float temporalChance = cornerPropPlacementChance;

            foreach (Vector2Int cornerTile in room.CornerTiles)
            {
                if (UnityEngine.Random.value < temporalChance)
                {
                    Prop propToPlace = cornerProps[UnityEngine.Random.Range(0, cornerProps.Count)];
                    PlacePropGameObjectAt(room, cornerTile, propToPlace);
                    if (propToPlace.PlaceAsGroup)
                    {
                        PlaceGroupObject(room, cornerTile, propToPlace, 2);
                    }
                }
                else
                {
                    temporalChance += Mathf.Clamp01(temporalChance + 0.1f);
                }
            }
        }

        /// <summary>
        /// This method is used to place object prop groups in the room.
        /// <paramref name="room"/> is the room to place the props in.
        /// <paramref name="groupOriginPosition"/> is the position of the prop.
        /// <paramref name="propToPlace"/> is the prop to place.
        /// <paramref name="searchOffset"/> is the offset to search for available positions.
        /// </summary>
        /// <param name="room"></param>
        /// <param name="groupOriginPosition"></param>
        /// <param name="propToPlace"></param>
        /// <param name="searchOffset"></param>
        private void PlaceGroupObject(Room room, Vector2Int groupOriginPosition, Prop propToPlace, int searchOffset)
        {
            int count = UnityEngine.Random.Range(propToPlace.GroupMinCount, propToPlace.GroupMaxCount) - 1;
            count = Mathf.Clamp(count, 0, 8);

            List<Vector2Int> availableSpaces = new List<Vector2Int>();

            for (int xOffset = -searchOffset; xOffset <= searchOffset; xOffset++)
            {
                for (int yOffset = -searchOffset; yOffset <= searchOffset; yOffset++)
                {
                    Vector2Int temporalPosition = groupOriginPosition + new Vector2Int(xOffset, yOffset);

                    if (room.FloorTiles.Contains(temporalPosition) &&
                        !mapData.Path.Contains(temporalPosition) &&
                        !room.PropPositions.Contains(temporalPosition))
                    {
                        availableSpaces.Add(temporalPosition);
                    }
                }
            }

            availableSpaces.OrderBy(x => Guid.NewGuid());

            int temporalCount = count < availableSpaces.Count ? count : availableSpaces.Count;

            for (short i = 0; i < temporalCount; i++)
                PlacePropGameObjectAt(room, availableSpaces[i], propToPlace);
        }

        /// <summary>
        /// This method is used to place the prop at a specific location in the room.
        /// <paramref name="room"/> is the room to place the prop in.
        /// <paramref name="placementPosition"/> is the position to place the prop at.
        /// <paramref name="propToPlace"/> is the prop to place.
        /// </summary>
        /// <param name="room"></param>
        /// <param name="placementPosition"></param>
        /// <param name="propToPlace"></param>
        /// <returns>
        /// The prop game object.
        /// </returns>
        private GameObject PlacePropGameObjectAt(Room room, Vector2Int placementPosition, Prop propToPlace)
        {
            GameObject prop = Instantiate(propPrefab);
            prop.name = $"{propToPlace.name}Prop";
            prop.transform.SetParent(parent);
            SpriteRenderer propSpriteRenderer = prop.GetComponentInChildren<SpriteRenderer>();
            propSpriteRenderer.sprite = propToPlace.PropSprite;
            propSpriteRenderer.sortingOrder = propToPlace.SortingLayerName == "Background" ? 10 : 0;
            propSpriteRenderer.sortingLayerID = SortingLayer.NameToID(propToPlace.SortingLayerName);

            if (propToPlace.HasCollider)
            {
                CapsuleCollider2D collider = prop.gameObject.AddComponent<CapsuleCollider2D>();
                collider.offset = propToPlace.ColliderOffset == Vector2.zero ? Vector2.zero : (Vector2)propToPlace.ColliderOffset;

                if (propToPlace.PropSize.x > propToPlace.PropSize.y)
                {
                    collider.direction = CapsuleDirection2D.Horizontal;
                }

                Vector2 size = propToPlace.ColliderSize == Vector2.zero ? new Vector2(propToPlace.PropSize.x * 0.8f, propToPlace.PropSize.y * 0.8f) : propToPlace.ColliderSize;

                collider.size = size;
            }

            prop.transform.position = (Vector2)placementPosition;
            propSpriteRenderer.transform.localPosition = (Vector2)propToPlace.PropSize * 0.5f;

            room.PropPositions.Add(placementPosition);
            room.PropObjectReferences.Add(prop);
            return prop;
        }
    }
}