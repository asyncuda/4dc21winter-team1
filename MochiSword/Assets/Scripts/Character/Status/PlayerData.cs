using UnityEngine;

namespace Character.Status {
    [CreateAssetMenu(menuName = "Database/PlayerData")]
    public class PlayerData : StatusData {
        [SerializeField] private int maxSpecialPoint = default;
        [SerializeField] private int attackInterval = default;
        [SerializeField] private int speed = default;
        [SerializeField] private int attackGetPoint = default;
        [SerializeField] private int hurtGetPoint = default;
        [SerializeField] private int specialTime = default;
        [SerializeField] private float powerBuffRate = default;
        [SerializeField] private float attackRangeBuffRate = default;

        public int MAXSpecialPoint => maxSpecialPoint;
        public int AttackInterval => attackInterval;
        public int Speed => speed;
        public int AttackGetPoint => attackGetPoint;
        public int HurtGetPoint => hurtGetPoint;
        public int SpecialTime => specialTime;
        public float PowerBuffRate => powerBuffRate;
        public float AttackRangeBuffRate => attackRangeBuffRate;
        
    }
}
