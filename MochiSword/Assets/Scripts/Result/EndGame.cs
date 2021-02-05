using Library.Scene;
using UniRx;
using UnityEngine;

namespace Result {
    public class EndGame : MonoBehaviour {
        private void Start() {
            Observable.EveryUpdate()
                .Where(_ => Input.anyKeyDown)
                .Subscribe(_ => SceneMover.MoveAsync(Scenes.Title).Forget())
                .AddTo(this);
        }
    }
}
