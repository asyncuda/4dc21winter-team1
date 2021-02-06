using Library.Scene;
using UniRx;
using UnityEngine;
using Zenject;
using Library.Audio;

namespace Enemy.Boss {
    /// <summary>
    /// Bossのコア部分の体力を管理するクラス
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class SoftBossHealth : MonoBehaviour, IReceivableStab {
        [SerializeField] private int health = default;

        [Inject] SePlayer sePlayer;
        [Inject] SoundDatabase soundDatabase;

        private void Start() {
            this.ObserveEveryValueChanged(x => x.health)
                .Where(x => x <= 0)
                .Subscribe(_ => {
                    sePlayer.PlayOneShot(soundDatabase.DefeatClip);
                    gameObject.SetActive(false);
                    SceneMover.MoveAsync(Scenes.Result).Forget();
                })
                .AddTo(this);
            
            
        }

        public void ReceiveDamage(int point) {
            health -= point;
        }
    }
}
