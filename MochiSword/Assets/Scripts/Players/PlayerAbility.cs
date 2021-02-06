using System;
using Library;
using UniRx;
using UnityEngine;

namespace Players {
    [RequireComponent(typeof(PlayerMediator))]
    public class PlayerAbility : MonoBehaviour {
        [SerializeField] private int maxSpecialPoint = default;
        [SerializeField] private int specialTime = default;
        private PlayerMediator mediator;
        private readonly FloatReactiveProperty specialPoint = new FloatReactiveProperty(0.0f);

        private void Start() {
            mediator = GetComponent<PlayerMediator>();

            specialPoint
                .Subscribe(x => mediator.OnSpecialChanged(x/maxSpecialPoint))
                .AddTo(this);

            // ポイントが最大になったらバフ
            specialPoint
                .Where(x => x >= maxSpecialPoint)
                .Subscribe(_ => {
                    mediator.StartBuff();
                    DecreasePoint();
                })
                .AddTo(this);

            // ポイントが0になったらバフ解除
            specialPoint
                .Skip(1)
                .Where(x => x <= 0)
                .Subscribe(_ => mediator.StopBuff())
                .AddTo(this);

            specialPoint.Subscribe(x => Debugger.Log(x)).AddTo(this);
        }

        public void CreasePoint(int point) {
            specialPoint.Value += point;
        }

        /// <summary>
        /// ポイントを減少させる
        /// </summary>
        private void DecreasePoint() {
            Observable.Interval(TimeSpan.FromSeconds(1))
                .TakeWhile(_ => specialPoint.Value >= 0)
                .Select(_ => (float) maxSpecialPoint/specialTime)
                .Subscribe(x => {
                    specialPoint.Value -= x;
                    if (specialPoint.Value <= 0) specialPoint.Value = 0;
                })
                .AddTo(this);
        }
    }
}
