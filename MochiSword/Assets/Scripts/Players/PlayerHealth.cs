using UnityEngine;

namespace Players {
    /// <summary>
    /// プレイヤーの体力を管理するクラス
    /// </summary>
    [RequireComponent(typeof(PlayerMediator))]
    [RequireComponent(typeof(Collider2D))]
    public class PlayerHealth : MonoBehaviour, IReceivableEnemyAttack {
        private PlayerMediator mediator;

        private void Start() {
            mediator = GetComponent<PlayerMediator>();
        }


        public void ReceiveDamage(int point) {
            mediator.Health -= point;
        }
    }
}
