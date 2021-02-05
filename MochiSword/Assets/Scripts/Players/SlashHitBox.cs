using Enemy;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Players {
    [RequireComponent(typeof(BoxCollider2D))]
    public class SlashHitBox : MonoBehaviour {
        [SerializeField] private float buffRate = default;
        [Inject] private PlayerMediator mediator = default;
        private BoxCollider2D boxCollider2D;
        private Vector2 colliderSize;

        private void Start() {
            boxCollider2D = GetComponent<BoxCollider2D>();
            colliderSize = boxCollider2D.size;
            
            this.OnTriggerEnter2DAsObservable()
                .Select(x => x.gameObject.GetComponent<IReceivableSlash>())
                .Where(x => x != null)
                .Subscribe(x => {
                    x.ReceiveDamage(mediator.Power);
                    mediator.OnAttackHit();
                })
                .AddTo(this);
        }

        public void StartBuff() {
            boxCollider2D.size = colliderSize * buffRate;
        }

        public void StopBuff() {
            boxCollider2D.size = colliderSize;
        }
    }
}
