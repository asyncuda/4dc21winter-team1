using UnityEngine;

namespace Enemy.SoftMochi {
    /// <summary>
    /// 柔らかい餅の体力を管理するクラス
    /// </summary>
    [RequireComponent(typeof(SoftMochiMediator))]
    [RequireComponent(typeof(Collider2D))]
    public class SoftMochiHealth : MonoBehaviour, IReceivableStab {
        private SoftMochiMediator mediator;

        private void Start() {
            mediator = GetComponent<SoftMochiMediator>();
        }

        public void ReceiveDamage(int point) {
            mediator.Health -= point;
        }
    }
}
