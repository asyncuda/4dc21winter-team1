using UnityEngine;

namespace Library.Audio {
    [CreateAssetMenu(menuName = "Database/SoundDatabase")]
    public class SoundDatabase : ScriptableObject {
        [SerializeField] private AudioClip jumpClip = default;

        public AudioClip JumpClip => jumpClip;
    }
}
