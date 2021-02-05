using UnityEngine;

namespace Library.Effects {
    [CreateAssetMenu(menuName = "Database/EffectDatabase")]
    public class EffectDatabase : ScriptableObject {
        [SerializeField] private GameObject slashEffect = default;
        [SerializeField] private GameObject specialSlashEffect = default;
        [SerializeField] private GameObject stabEffect = default;
        [SerializeField] private GameObject specialStabEffect = default;
        [SerializeField] private GameObject enemyDeathEffect = default;

        public GameObject SlashEffect => slashEffect;
        public GameObject SpecialSlashEffect => specialSlashEffect;
        public GameObject StabEffect => stabEffect;
        public GameObject SpecialStabEffect => specialStabEffect;
        public GameObject EnemyDeathEffect => enemyDeathEffect;
    }
}
