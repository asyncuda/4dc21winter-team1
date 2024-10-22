﻿using Enemy;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;
using Library.Audio;

namespace Players {
    [RequireComponent(typeof(BoxCollider2D))]
    public class StabHitBox : MonoBehaviour {
        [SerializeField] private float buffRate = default;
        [Inject] private PlayerMediator mediator = default;
        private BoxCollider2D boxCollider2D;
        private Vector2 colliderSize;

        [Inject] SePlayer sePlayer;
        [Inject] SoundDatabase soundDatabase;

        private void Start() {
            boxCollider2D = GetComponent<BoxCollider2D>();
            colliderSize = boxCollider2D.size;
            
            this.OnTriggerEnter2DAsObservable()
                .Select(x => x.gameObject.GetComponent<IReceivableStab>())
                .Where(x => x != null)
                .Subscribe(x => {
                    sePlayer.PlayOneShot(soundDatabase.KillEnemyClip);
                    x.ReceiveDamage(mediator.Power);
                    mediator.OnAttackHit();
                })
                .AddTo(this);
            gameObject.SetActive(false);
        }

        public void StartBuff() {
            boxCollider2D.size = colliderSize * buffRate;
        }

        public void StopBuff() {
            boxCollider2D.size = colliderSize;
        }
    }
}
