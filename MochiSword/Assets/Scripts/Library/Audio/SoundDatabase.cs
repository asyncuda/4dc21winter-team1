using UnityEngine;

namespace Library.Audio {
    [CreateAssetMenu(menuName = "Database/SoundDatabase")]
    public class SoundDatabase : ScriptableObject {
        [SerializeField] private AudioClip jumpClip = default;
        [SerializeField] private AudioClip damageClip = default;
        [SerializeField] private AudioClip killEnemyClip = default;
        [SerializeField] private AudioClip furiClip = default;
        [SerializeField] private AudioClip tukiClip = default;
        [SerializeField] private AudioClip gameStartClip = default;
        [SerializeField] private AudioClip defeatClip = default;


        public AudioClip JumpClip => jumpClip;
        public AudioClip DamageClip => damageClip;
        public AudioClip KillEnemyClip => killEnemyClip;
        public AudioClip FuriClip => furiClip;
        public AudioClip TukiClip => tukiClip;
        public AudioClip GameStartClip => gameStartClip;
        public AudioClip DefeatClip => defeatClip;
    }
}
