using Players;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Enemy.Boss {
    [RequireComponent(typeof(BossMediator))]
    [RequireComponent(typeof(Collider2D))]
    public class BossHitBox : MonoBehaviour {
        private void Start() {
            var mediator = GetComponent<BossMediator>();

            // 接触したオブジェクトが特定のインターフェイスを実装していればダメージを与える
            this.OnTriggerEnter2DAsObservable()
                .Select(x => x.gameObject.GetComponent<IReceivableEnemyAttack>())
                .Where(x => x != null)
                .Subscribe(x => x.ReceiveDamage(mediator.Power))
                .AddTo(this);
        }
    }
}
