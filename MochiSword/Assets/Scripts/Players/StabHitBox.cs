using Enemy;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Players {
    [RequireComponent(typeof(PlayerMediator))]
    [RequireComponent(typeof(Collider2D))]
    public class StabHitBox : MonoBehaviour {
        [Inject] private PlayerMediator mediator = default;
        private void Start() {
            // 接触した敵が特定のインターフェイスを実装していたらダメージを与える
            this.OnTriggerEnter2DAsObservable()
                .Select(x => x.gameObject.GetComponent<IReceivableStab>())
                .Where(x => x != null)
                .Subscribe(x => x.ReceiveDamage(mediator.power))
                .AddTo(this);
        }
    }
}
