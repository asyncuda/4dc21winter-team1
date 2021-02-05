using UniRx;
using UnityEngine;

namespace Enemy.Boss {
    /// <summary>
    /// Bossの硬い部分の体力を管理するクラス
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class HardBossHealth : MonoBehaviour, IReceivableSlash {
        [SerializeField] private int health = default;

        private void Start() {
            this.ObserveEveryValueChanged(x => x.health)
                .Where(x => x <= 0)
                .Subscribe(_ => gameObject.SetActive(false))
                .AddTo(this);
        }

        public void ReceiveDamage(int point) {
            health -= point;
        }
    }
}
