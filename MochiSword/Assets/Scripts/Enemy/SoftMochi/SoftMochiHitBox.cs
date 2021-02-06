using Players;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;
using Library.Audio;

namespace Enemy.SoftMochi {
    [RequireComponent(typeof(Collider2D))]
    public class SoftMochiHitBox : MonoBehaviour {
        [SerializeField] private int power = default;

        [Inject] SePlayer sePlayer;
        [Inject] SoundDatabase soundDatabase;

        private void Start() {
            // 接触したオブジェクトが特定のインターフェイスを実装していたらダメージを与える
            this.OnTriggerEnter2DAsObservable()
                .Select(x => x.gameObject.GetComponent<IReceivableEnemyAttack>())
                .Where(x => x != null)
                .Subscribe(x => {
                    //sePlayer.PlayOneShot(soundDatabase.KillEnemyClip);
                    x.ReceiveDamage(power);
                })
                .AddTo(this);
        }
    }
}
