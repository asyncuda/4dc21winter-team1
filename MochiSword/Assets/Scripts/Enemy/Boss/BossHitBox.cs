using Players;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Enemy.Boss {
    [RequireComponent(typeof(Collider2D))]
    public class BossHitBox : MonoBehaviour {
        [SerializeField] private int power = default;
        
        private void Start() {
            var mediator = GetComponent<BossMediator>();

            // 接触したオブジェクトが特定のインターフェイスを実装していればダメージを与える
            this.OnTriggerEnter2DAsObservable()
                .Select(x => x.gameObject.GetComponent<IReceivableEnemyAttack>())
                .Where(x => x != null)
                .Subscribe(x => x.ReceiveDamage(power))
                .AddTo(this);
        }
    }
}
