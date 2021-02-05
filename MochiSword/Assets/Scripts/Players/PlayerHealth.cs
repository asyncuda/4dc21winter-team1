using Library.Scene;
using UniRx;
using UnityEngine;

namespace Players {
    /// <summary>
    /// プレイヤーの体力を管理するクラス
    /// </summary>
    [RequireComponent(typeof(PlayerMediator))]
    [RequireComponent(typeof(Collider2D))]
    public class PlayerHealth : MonoBehaviour, IReceivableEnemyAttack {
        [SerializeField] private int health = default;
        private PlayerMediator mediator;

        private void Start() {
            mediator = GetComponent<PlayerMediator>();

            this.ObserveEveryValueChanged(x => x.health)
                .Where(x => x <= 0)
                .Subscribe(_ => SceneMover.Restart())
                .AddTo(this);
        }
        
        public void ReceiveDamage(int point) {
            health -= point;
            mediator.OnHealthChanged(health);
        }
    }
}
