using System;
using Character;
using Enemy;
using Library;
using UniRx;
using UnityEngine;
using Zenject;

namespace Players {
    /// <summary>
    /// プレイヤーの情報をまとめるクラス
    /// </summary>
    public class PlayerMediator : MediatorBase {
        [SerializeField] private int power = default;
        [SerializeField] private float buffRate = default;
        [SerializeField] private int attackSpecialPoint = default;
        [SerializeField] private int hurtSpecialPoint = default;
        [Inject] private StabHitBox stabHitBox = default;
        [Inject] private SlashHitBox slashHitBox = default;
        private readonly Subject<float> percentageSubject = new Subject<float>();
        public IObservable<float> OnSpecialPercentageChanged => percentageSubject;
        public bool isBuffing { get; private set; }
        private PlayerAbility playerAbility;

        public int Health { get; private set; }
        public int Power { get; private set; }
        public float SpecialPercentage { get; private set; }

        private void Awake() {
            Initialize();
            playerAbility = GetComponent<PlayerAbility>();
            this.ObserveEveryValueChanged(x => isBuffing)
                .Subscribe(x => Power = (int) (power * (x ? buffRate : 1)))
                .AddTo(this);
        }

        public void OnAttackHit() {
            playerAbility.CreasePoint(attackSpecialPoint);
        }

        public void OnHealthChanged(int health) {
            playerAbility.CreasePoint(hurtSpecialPoint);
            Health = health;
        }

        public void OnSpecialChanged(float percent) {
            percentageSubject.OnNext(percent);
        }

        public void StartBuff() {
            isBuffing = true;
            slashHitBox.StartBuff();
            stabHitBox.StartBuff();
        }

        public void StopBuff() {
            isBuffing = false;
            slashHitBox.StopBuff();
            stabHitBox.StartBuff();
        }
    }
}
