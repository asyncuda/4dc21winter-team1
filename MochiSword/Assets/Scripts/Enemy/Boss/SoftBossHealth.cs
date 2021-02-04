using UnityEngine;

namespace Enemy.Boss {
    /// <summary>
    /// Bossのコア部分の体力を管理するクラス
    /// </summary>
    [RequireComponent(typeof(BossMediator))]
    [RequireComponent(typeof(Collider2D))]
    public class SoftBossHealth : MonoBehaviour, IReceivableStab {
        private BossMediator mediator;

        private void Start() {
            mediator = GetComponent<BossMediator>();
        }

        public void ReceiveDamage(int point) {
            mediator.Health -= point;
        }
    }
}
