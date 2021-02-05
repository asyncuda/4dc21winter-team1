using System;
using Character;
using Character.Status;
using Enemy;
using UniRx;
using UnityEngine;
using Zenject;

namespace Players {
    /// <summary>
    /// プレイヤーの情報をまとめるクラス
    /// </summary>
    public class PlayerMediator : MonoBehaviour {
        [Inject] private StatusDatabase database;
        public IObservable<IReceivableSlash> OnSlashHit;
        public IObservable<IReceivableStab> OnStabHit;
        public IObservable<int> OnHealthChanged;
        public IObservable<float> OnSpecialPercentageChanged;
        public IObservable<bool> IsBuffing;

        public PlayerData PlayerData { get; private set; }
        public int Health { get; private set; }
        public int Power { get; private set; }
        public float SpecialPercentage { get; private set; }

        private void Start() {
            PlayerData = database.GetData(CharacterType.Player) as PlayerData;
            if (PlayerData == null) return;
            Health = PlayerData.Health;
            Power = PlayerData.Power;

            IsBuffing
                .Subscribe(x => Power = (int) (PlayerData.Power * (x ? PlayerData.PowerBuffRate : 1)))
                .AddTo(this);
        }
    }
}
