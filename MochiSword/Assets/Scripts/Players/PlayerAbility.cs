using System;
using Cysharp.Threading.Tasks;
using Library;
using Library.Audio;
using Library.Pause;
using UniRx;
using UnityEngine;
using Zenject;

namespace Players {
    [RequireComponent(typeof(PlayerMediator))]
    public class PlayerAbility : MonoBehaviour {
        [SerializeField] private GameObject cutIn = default;
        [SerializeField] private int maxSpecialPoint = default;
        [SerializeField] private int specialTime = default;
        [SerializeField] private int cutInTimeMs = default;
        [Inject] private BgmPlayer bgmPlayer = default;
        [Inject] private SoundDatabase soundDatabase = default;
        private PlayerMediator mediator;
        private readonly FloatReactiveProperty specialPoint = new FloatReactiveProperty(0.0f);
        private bool isBuffing;

        private void Start() {
            mediator = GetComponent<PlayerMediator>();

            specialPoint
                .Subscribe(x => mediator.OnSpecialChanged(x/maxSpecialPoint))
                .AddTo(this);

            // ポイントが最大になったらバフ
            specialPoint
                .Where(x => x >= maxSpecialPoint && !isBuffing)
                .Subscribe(_ => {
                    isBuffing = true;
                    mediator.StartBuff();
                    CutInAsync().Forget();
                })
                .AddTo(this);

            // ポイントが0になったらバフ解除
            specialPoint
                .Where(x => x <= 0 && isBuffing)
                .Subscribe(_ => {
                    isBuffing = false;
                    mediator.StopBuff();
                    bgmPlayer.Play(soundDatabase.MainBgm);
                })
                .AddTo(this);
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
                    Debugger.Log("point: " + specialPoint.Value);
                })
                .AddTo(this);
        }

        private async UniTaskVoid CutInAsync() {
            Pauser.Pause();
            cutIn.SetActive(true);
            await UniTask.Delay(cutInTimeMs);
            cutIn.SetActive(false);
            Pauser.Resume();
            bgmPlayer.Play(soundDatabase.BuffBgm);
            DecreasePoint();
        }
    }
}
