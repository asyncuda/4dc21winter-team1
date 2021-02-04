using UnityEngine;

namespace Enemy.Boss {
    /// <summary>
    /// Bossの硬い部分の体力を管理するクラス
    /// </summary>
    [RequireComponent(typeof(BossMediator))]
    [RequireComponent(typeof(Collider2D))]
    public class HardBossHealth : MonoBehaviour, IReceivableSlash {
        private BossMediator mediator;

        private void Start() {
            mediator = GetComponent<BossMediator>();
        }

        public void ReceiveDamage(int point) {
            mediator.Health -= point;
        }
    }
}
