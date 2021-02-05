using Enemy;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Players {
    [RequireComponent(typeof(PlayerMediator))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class StabHitBox : MonoBehaviour {
        [Inject] private PlayerMediator mediator = default;

        private void Start() {
            // 接触した敵が特定のインターフェイスを実装していたら通知
            mediator.OnStabHit = this.OnTriggerEnter2DAsObservable()
                .Select(x => x.gameObject.GetComponent<IReceivableStab>())
                .Where(x => x != null);

            // 通知が来たらダメージを与える
            mediator.OnStabHit
                .Subscribe(x => x.ReceiveDamage(mediator.Power))
                .AddTo(this);
            
            // スペシャルが溜まったら判定を大きく
            var boxCollider = GetComponent<BoxCollider2D>();
            var size = boxCollider.size;
            mediator.IsBuffing
                .Subscribe(x => boxCollider.size = size * (x ? mediator.PlayerData.AttackRangeBuffRate : 1))
                .AddTo(this);
        }
    }
}
