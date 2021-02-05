using UnityEngine;

namespace Enemy.SoftMochi {
    /// <summary>
    /// 柔らかい餅の体力を管理するクラス
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class SoftMochiHealth : MonoBehaviour, IReceivableStab {
        [SerializeField] private int health = default;

        public void ReceiveDamage(int point) {
            health -= point;
        }
    }
}
