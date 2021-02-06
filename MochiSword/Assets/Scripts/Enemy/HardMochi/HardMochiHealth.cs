using Library.Effects;
using UniRx;
using UnityEngine;
using Zenject;
using Library.Audio;

namespace Enemy.HardMochi {
    /// <summary>
    /// 硬い餅の体力を管理するクラス
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class HardMochiHealth : MonoBehaviour, IReceivableSlash {
        [SerializeField] private int health = default;
        [Inject] private EffectPlayer effectPlayer = default;
        [Inject] private EffectDatabase effectDatabase = default;

        [Inject] SePlayer sePlayer;
        [Inject] SoundDatabase soundDatabase;

        private void Start() {
            this.ObserveEveryValueChanged(x => health)
                .Where(x => x <= 0)
                .Subscribe(_ => {
                    effectPlayer.Play(effectDatabase.EnemyDefeatEffect, transform.position);
                    gameObject.SetActive(false);
                })
                .AddTo(this);
        }

        public void ReceiveDamage(int point) {
            health -= point;
        }
    }
}
