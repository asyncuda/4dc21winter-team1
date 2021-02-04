using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Enemy.HardMochi {
    [RequireComponent(typeof(HardMochiMediator))]
    [RequireComponent(typeof(Collider2D))]
    public class HardMochiHitBox : MonoBehaviour {
        private void Start() {
            var mediator = GetComponent<HardMochiMediator>();

            // 接触したオブジェクトが特定のインターフェイスを実装していたらダメージを与える
            this.OnTriggerEnter2DAsObservable()
                .Select(x => x.gameObject.GetComponent<IReceivableSlash>())
                .Where(x => x != null)
                .Subscribe(x => x.ReceiveDamage(mediator.Power))
                .AddTo(this);
        }
    }
}
