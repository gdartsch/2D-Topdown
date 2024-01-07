using UnityEngine;

namespace MatchaIsSpent.Characters.FootstepsSystem
{
    /// <summary>
    /// This class is used to play the footstep sound when the footstep animation event is called.
    /// </summary>
    public class FootstepAnimationEventHandler : MonoBehaviour
    {
        [Tooltip("The audio source that plays the footstep sounds.")]
        [SerializeField] private AudioSource audioSource;
        [Tooltip("The footsteps map manager.")]
        [SerializeField] private FootstepsMapManager footstepsMapManager;

        private void Start()
        {
            if (footstepsMapManager == null)
                footstepsMapManager = FindObjectOfType<FootstepsMapManager>();
        }

        /// <summary>
        /// Play the footstep sound. This method is called when the footstep animation event is called.
        /// </summary>
        public void PlayFootstepSound()
        {
            AudioClip clip = footstepsMapManager.GetCurrentFloorClip(transform.position);

            if (clip != null)
                audioSource.PlayOneShot(clip);
        }
    }
}