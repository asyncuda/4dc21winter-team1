using UnityEngine;

namespace Enemy.HardMochi {
    /// <summary>
    /// 硬い餅の体力を管理するクラス
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class HardMochiHealth : MonoBehaviour, IReceivableSlash {
        [SerializeField] private int health = default;

        public void ReceiveDamage(int point) {
            health -= point;
        }
    }
}
