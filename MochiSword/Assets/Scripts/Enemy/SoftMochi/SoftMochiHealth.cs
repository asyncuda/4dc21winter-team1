using Library.Effects;
using UniRx;
using UnityEngine;
using Zenject;
using Library.Audio;

namespace Enemy.SoftMochi {
    /// <summary>
    /// 柔らかい餅の体力を管理するクラス
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class SoftMochiHealth : MonoBehaviour, IReceivableStab {
        [SerializeField] private int health = default;
        [Inject] private EffectPlayer effectPlayer = default;
        [Inject] private EffectDatabase effectDatabase = default;

        [Inject] SePlayer sePlayer;
        [Inject] SoundDatabase soundDatabase;

        private void Start() {
            this.ObserveEveryValueChanged(x => x.health)
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
