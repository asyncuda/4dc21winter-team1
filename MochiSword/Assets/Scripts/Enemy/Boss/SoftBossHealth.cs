using UnityEngine;

namespace Enemy.Boss {
    /// <summary>
    /// Bossのコア部分の体力を管理するクラス
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class SoftBossHealth : MonoBehaviour, IReceivableStab {
        [SerializeField] private int health = default; 

        public void ReceiveDamage(int point) {
            health -= point;
        }
    }
}
