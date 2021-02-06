using Library.Scene;
using UniRx;
using UnityEngine;
using Zenject;
using Library.Audio;

namespace Players {
    /// <summary>
    /// プレイヤーの体力を管理するクラス
    /// </summary>
    [RequireComponent(typeof(PlayerMediator))]
    [RequireComponent(typeof(Collider2D))]
    public class PlayerHealth : MonoBehaviour, IReceivableEnemyAttack {
        [SerializeField] private int health = default;
        private PlayerMediator mediator;

        [Inject] SePlayer sePlayer;
        [Inject] SoundDatabase soundDatabase;

        private void Start() {

            mediator = GetComponent<PlayerMediator>();

            this.ObserveEveryValueChanged(x => x.health)
                .Where(x => x <= 0)
                .Subscribe(_ => SceneMover.Restart().Forget())
                .AddTo(this);
        }
        
        public void ReceiveDamage(int point) {
            sePlayer.PlayOneShot(soundDatabase.DamageClip);
            health -= point;
            mediator.OnHealthChanged(health);
        }
    }
}
