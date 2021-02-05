using UniRx;
using UnityEngine;

namespace Players {
    /// <summary>
    /// プレイヤーの体力を管理するクラス
    /// </summary>
    [RequireComponent(typeof(PlayerMediator))]
    [RequireComponent(typeof(Collider2D))]
    public class PlayerHealth : MonoBehaviour, IReceivableEnemyAttack {
        private readonly IntReactiveProperty health = new IntReactiveProperty();
        private PlayerMediator mediator;

        private void Start() {
            mediator = GetComponent<PlayerMediator>();
            health.Value = mediator.Health;

            // 体力が変わったら通知
            mediator.OnHealthChanged = health;
        }


        public void ReceiveDamage(int point) {
            health.Value -= point;
        }
    }
}
