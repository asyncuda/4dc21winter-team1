using UnityEngine;

namespace Character.Status {
    [CreateAssetMenu(menuName = "Database/StatusData")]
    public class StatusData : ScriptableObject {
        [SerializeField] protected CharacterType type = default;
        [SerializeField] protected int health = default;
        [SerializeField] protected int power = default;
        [SerializeField] protected float jumpPower = default;

        public CharacterType Type => type;
        public int Health => health;
        public int Power => power;
        public float JumpPower => jumpPower;
    }
}
