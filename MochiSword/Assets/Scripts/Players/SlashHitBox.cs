using Enemy;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Players {
    [RequireComponent(typeof(PlayerMediator))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class SlashHitBox : MonoBehaviour {
        [SerializeField] private float buffRate = default;
        [Inject] private PlayerMediator mediator = default;

        private void Start() {
            // 接触したオブジェクトが特定のインターフェイスを実装していたら通知する
            mediator.OnSlashHit = this.OnTriggerEnter2DAsObservable()
                .Select(x => x.gameObject.GetComponent<IReceivableSlash>())
                .Where(x => x != null);

            // 通知が来たらダメージを与える
            mediator.OnSlashHit
                .Subscribe(x => x.ReceiveDamage(mediator.Power))
                .AddTo(this);

            // スペシャルが溜まったら判定を大きく
            var boxCollider2D = GetComponent<BoxCollider2D>();
            var size = boxCollider2D.size;
            mediator.IsBuffing
                .Subscribe(x => boxCollider2D.size = size * (x ? buffRate : 1))
                .AddTo(this);
        }
    }
}
