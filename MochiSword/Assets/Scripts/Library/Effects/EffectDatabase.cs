using UnityEngine;

namespace Library.Effects {
    [CreateAssetMenu(menuName = "Database/EffectDatabase")]
    public class EffectDatabase : ScriptableObject {
        [SerializeField] private GameObject slashEffect = default;
        [SerializeField] private GameObject buffSlashEffect = default;
        [SerializeField] private GameObject spearEffect = default;
        [SerializeField] private GameObject buffSpearEffect = default;
        [SerializeField] private GameObject enemyDefeatEffect = default;

        public GameObject SlashEffect => slashEffect;
        public GameObject BuffSlashEffect => buffSlashEffect;
        public GameObject SpearEffect => spearEffect;
        public GameObject BuffSpearEffect => buffSpearEffect;
        public GameObject EnemyDefeatEffect => enemyDefeatEffect;
    }
}
