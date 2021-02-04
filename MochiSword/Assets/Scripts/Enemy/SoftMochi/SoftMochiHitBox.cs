using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Enemy.SoftMochi {
    [RequireComponent(typeof(SoftMochiMediator))]
    [RequireComponent(typeof(Collider2D))]
    public class SoftMochiHitBox : MonoBehaviour {
        private void Start() {
            var mediator = GetComponent<SoftMochiMediator>();

            // 接触したオブジェクトが特定のインターフェイスを実装していたらダメージを与える
            this.OnTriggerEnter2DAsObservable()
                .Select(x => x.gameObject.GetComponent<IReceivableStab>())
                .Where(x => x != null)
                .Subscribe(x => x.ReceiveDamage(mediator.Power))
                .AddTo(this);
        }
    }
}
