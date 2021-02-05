using UniRx;
using UnityEngine;

namespace Players {
    [RequireComponent(typeof(PlayerMediator))]
    public class PlayerAbility : MonoBehaviour {
        [SerializeField] private int maxSpecialPoint = default;
        [SerializeField] private int attackGetPoint = default;
        [SerializeField] private int hurtGetPoint = default;
        [SerializeField] private int specialTime = default;
        private PlayerMediator mediator;
        private readonly FloatReactiveProperty specialPoint = new FloatReactiveProperty();
        private bool isBuffing;

        private void Start() {
            mediator = GetComponent<PlayerMediator>();
            mediator.OnSpecialPercentageChanged = specialPoint.Select(x => x / maxSpecialPoint);

            // 攻撃が当たったらポイント上昇
            mediator.OnSlashHit.AsUnitObservable()
                .Merge(mediator.OnStabHit.AsUnitObservable())
                .Subscribe(_ => specialPoint.Value += attackGetPoint)
                .AddTo(this);

            // ダメージを受けたらポイント上昇
            mediator.OnHealthChanged
                .Subscribe(_ => specialPoint.Value += hurtGetPoint)
                .AddTo(this);

            // ポイントが最大になったらバフ
            mediator.IsBuffing = specialPoint
                .Where(x => x >= maxSpecialPoint)
                .Select(_ => true);

            // ポイントが0になったらバフ解除
            mediator.IsBuffing = specialPoint
                .Where(x => x <= 0)
                .Select(_ => false);

            // 
            mediator.IsBuffing
                .Where(x => x)
                .Subscribe(_ => {
                    isBuffing = true;
                    DecreasePoint();
                })
                .AddTo(this);
        }

        /// <summary>
        /// ポイントを減少させる
        /// </summary>
        private void DecreasePoint() {
            Observable.EveryFixedUpdate()
                .TakeWhile(_ => isBuffing)
                .Select(_ => (float) maxSpecialPoint/specialTime)
                .Subscribe(x => specialPoint.Value -= x * Time.deltaTime)
                .AddTo(this);
        }
    }
}
