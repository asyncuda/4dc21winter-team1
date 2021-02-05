using UnityEngine;

namespace Enemy.Boss {
    /// <summary>
    /// Bossの硬い部分の体力を管理するクラス
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class HardBossHealth : MonoBehaviour, IReceivableSlash {
        [SerializeField] private int health = default;

        public void ReceiveDamage(int point) {
            health -= point;
        }
    }
}
