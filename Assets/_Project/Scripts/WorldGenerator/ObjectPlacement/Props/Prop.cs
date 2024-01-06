using UnityEngine;

namespace MatchaIsSpent.WorldGeneration
{
    /// <summary>
    /// Scriptable object that holds data for a prop.
    /// </summary>
    [CreateAssetMenu(fileName = "Prop", menuName = "MatchaIsSpent/WorldGeneration/Prop", order = 1)]
    public class Prop : ScriptableObject
    {
        [field: Header("Prop Data")]
        [Tooltip("The sprite that will be used for the prop.")]
        [field: SerializeField] public Sprite PropSprite { get; private set; }
        [field: Tooltip("The size of the prop.")]
        [field: SerializeField] public Vector2Int PropSize { get; private set; }
        [field: Tooltip("The size of the prop collider.")]
        [field: SerializeField] public Vector2 ColliderSize { get; private set; } = Vector2.zero;
        [field: Tooltip("The offset of the prop.")]
        [field: SerializeField] public Vector2 ColliderOffset { get; private set; } = Vector2.zero;
        [field: Tooltip("Has the prop a collider?")]
        [field: SerializeField] public bool HasCollider { get; private set; } = true;
        [field: Tooltip("The layer of the prop.")]
        [field: SerializeField] public string SortingLayerName { get; private set; } = "Foreground";

        [field: Space]
        [field: Header("Placement type:")]
        [Tooltip("Can the prop be placed in a corner?")]
        [field: SerializeField] public bool Corner { get; private set; } = true;
        [field: Tooltip("Can the prop be placed near a wall up?")]
        [field: SerializeField] public bool NearWallUp { get; private set; } = true;
        [field: Tooltip("Can the prop be placed near a wall down?")]
        [field: SerializeField] public bool NearWallDown { get; private set; } = true;
        [field: Tooltip("Can the prop be placed near a wall left?")]
        [field: SerializeField] public bool NearWallLeft { get; private set; } = true;
        [field: Tooltip("Can the prop be placed near a wall right?")]
        [field: SerializeField] public bool NearWallRight { get; private set; } = true;
        [field: Tooltip("Can the prop be placed near a wall?")]
        [field: SerializeField] public bool Inner { get; private set; } = true;

        [field: Min(1)]
        [Tooltip("The minimum quantity of props.")]
        [field: SerializeField] public int PlacementQuantityMin { get; private set; } = 1;
        [Tooltip("The maximum quantity of props.")]
        [field: SerializeField] public int PlacementQuantityMax { get; private set; } = 1;

        [field: Space]
        [field: Header("Group placement:")]
        [Tooltip("Can the prop be placed as a group?")]
        [field: SerializeField] public bool PlaceAsGroup { get; private set; } = false;
        [field: Min(1)]
        [Tooltip("The minimum quantity of props in a group.")]
        [field: SerializeField] public int GroupMinCount { get; private set; } = 1;
        [Tooltip("The maximum quantity of props in a group.")]
        [field: SerializeField] public int GroupMaxCount { get; private set; } = 1;
    }
}
