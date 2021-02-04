using UnityEngine;

namespace Enemy.HardMochi {
    /// <summary>
    /// 硬い餅の体力を管理するクラス
    /// </summary>
    [RequireComponent(typeof(HardMochiMediator))]
    [RequireComponent(typeof(Collider2D))]
    public class HardMochiHealth : MonoBehaviour, IReceivableSlash {
        private HardMochiMediator mediator;

        private void Start() {
            mediator = GetComponent<HardMochiMediator>();
        }

        public void ReceiveDamage(int point) {
            mediator.Health -= point;
        }
    }
}
