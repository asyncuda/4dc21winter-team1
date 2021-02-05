using System;
using Enemy;
using UniRx;
using UnityEngine;

namespace Players {
    /// <summary>
    /// プレイヤーの情報をまとめるクラス
    /// </summary>
    public class PlayerMediator : MonoBehaviour {
        [SerializeField] private int power = default;
        [SerializeField] private float buffRate = default;
        public IObservable<IReceivableSlash> OnSlashHit;
        public IObservable<IReceivableStab> OnStabHit;
        public IObservable<int> OnHealthChanged;
        public IObservable<float> OnSpecialPercentageChanged;
        public IObservable<bool> IsBuffing;

        public int Health { get; private set; }
        public int Power { get; private set; }
        public float SpecialPercentage { get; private set; }

        private void Start() {
            IsBuffing
                .Subscribe(x => Power = (int) (power * (x ? buffRate : 1)))
                .AddTo(this);
        }
    }
}
