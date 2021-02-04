using UniRx;
using UnityEngine;

namespace Library {
    public class ForceQuitGame : MonoBehaviour {
        private void Start() {
            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.Escape))
                .Subscribe(_ => Application.Quit())
                .AddTo(this);
        }
    }
}
