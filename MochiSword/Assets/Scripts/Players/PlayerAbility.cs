using UniRx;
using UnityEngine;

namespace Players {
    public class PlayerAbility : MonoBehaviour {
        [SerializeField] private int decreaseTime = default;
        private PlayerMediator mediator;
        private readonly FloatReactiveProperty specialPoint = new FloatReactiveProperty();
        private bool isBuffing;
        private int maxSpecialPoint;

        private void Start() {
            mediator = GetComponent<PlayerMediator>();
            maxSpecialPoint = mediator.PlayerData.MAXSpecialPoint;
            mediator.OnSpecialPercentageChanged = specialPoint.Select(x =>(float) x / maxSpecialPoint);

            // 攻撃が当たったらポイント上昇
            mediator.OnSlashHit.AsUnitObservable()
                .Merge(mediator.OnStabHit.AsUnitObservable())
                .Subscribe(_ => specialPoint.Value += mediator.PlayerData.AttackGetPoint)
                .AddTo(this);

            // ダメージを受けたらポイント上昇
            mediator.OnHealthChanged
                .Subscribe(_ => specialPoint.Value += mediator.PlayerData.HurtGetPoint)
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
                .Select(_ => (float) maxSpecialPoint/decreaseTime)
                .Subscribe(x => specialPoint.Value -= x * Time.deltaTime)
                .AddTo(this);
        }
    }
}
