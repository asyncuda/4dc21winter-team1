using Library.Scene;
using UniRx;
using UnityEngine;

namespace Title {
    public class StartGame : MonoBehaviour {
        private void Start() {
            Observable.EveryUpdate()
                .Where(_ => Input.anyKey)
                .Subscribe(_ => SceneMover.MoveAsync(Scenes.Game).Forget())
                .AddTo(this);
        }
    }
}
