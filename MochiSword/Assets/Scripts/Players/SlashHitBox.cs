using Enemy;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Players {
    [RequireComponent(typeof(PlayerMediator))]
    [RequireComponent(typeof(Collider2D))]
    public class SlashHitBox : MonoBehaviour {
        [Inject] private PlayerMediator mediator = default;
        
        private void Start() {
            // 接触したオブジェクトが特定のインターフェイスを実装していたらダメージを与える
            this.OnTriggerEnter2DAsObservable()
                .Select(x => x.gameObject.GetComponent<IReceivableSlash>())
                .Where(x => x != null)
                .Subscribe(x => x.ReceiveDamage(mediator.power))
                .AddTo(this);
        }
    }
}
