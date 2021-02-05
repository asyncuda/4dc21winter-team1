using Players;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Enemy.HardMochi {
    [RequireComponent(typeof(Collider2D))]
    public class HardMochiHitBox : MonoBehaviour {
        [SerializeField] private int power = default;
        
        private void Start() {
            // 接触したオブジェクトが特定のインターフェイスを実装していたらダメージを与える
            this.OnTriggerEnter2DAsObservable()
                .Select(x => x.gameObject.GetComponent<IReceivableEnemyAttack>())
                .Where(x => x != null)
                .Subscribe(x => x.ReceiveDamage(power))
                .AddTo(this);
        }
    }
}
