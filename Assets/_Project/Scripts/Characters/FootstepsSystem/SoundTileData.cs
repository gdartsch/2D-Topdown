using UnityEngine;
using UnityEngine.Tilemaps;

namespace MatchaIsSpent.Characters.FootstepsSystem
{
    /// <summary>
    /// The type of floor this tile represents.
    /// </summary>
    public enum FloorType
    {
        Dirt,
        Grass
    }

    /// <summary>
    /// This class is used to store data about a sound tile.
    /// </summary>
    [CreateAssetMenu(fileName = "SoundTileData", menuName = "MatchaIsSpent/FootstepSystem//SoundTileData")]
    public class SoundTileData : ScriptableObject
    {
        [Tooltip("The priority of this tile. The higher the value, the higher the priority.")]
        [SerializeField] public int priority = 0;
        [Tooltip("The type of floor this tile represents.")]
        [SerializeField] public FloorType floorType;
        [Tooltip("The audio clip that plays when the player walks on this tile.")]
        [SerializeField] public AudioClip[] footstepSounds;
        [Tooltip("The tiles of the game")]
        [SerializeField] public TileBase[] tiles;
    }
}