using UnityEngine;

namespace Character.Status {
    [CreateAssetMenu(menuName = "Database/PlayerData")]
    public class PlayerData : StatusData {
        [SerializeField] private int maxSpecialPoint = default;
        [SerializeField] private int attackInterval = default;
        [SerializeField] private int speed = default;

        public int MAXSpecialPoint => maxSpecialPoint;
        public int AttackInterval => attackInterval;
        public int Speed => speed;
    }
}
