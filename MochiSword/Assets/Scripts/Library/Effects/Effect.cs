using UniRx;
using UnityEngine;

namespace Library.Effects {
    public class Effect : MonoBehaviour {
        private void Start() {
            var animator = GetComponent<Animator>();

            // アニメーションが終了したら非表示にする
            Observable.EveryFixedUpdate()
                .Where(_ => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                .Subscribe(_ => gameObject.SetActive(false))
                .AddTo(this);
        }
    }
}
