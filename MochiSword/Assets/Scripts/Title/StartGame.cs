using Library.Scene;
using UniRx;
using UnityEngine;
using Zenject;
using Library.Audio;

namespace Title {
    public class StartGame : MonoBehaviour {

        [Inject] SePlayer sePlayer;
        [Inject] SoundDatabase soundDatabase;

        private bool once = false;

        private void Start() {
            Observable.EveryUpdate()
                .Where(_ => Input.anyKey)
                .Subscribe(_ => {
                    SceneMover.MoveAsync(Scenes.Game).Forget();
                    if (!once) {
                        once = true;
                        sePlayer.PlayOneShot(soundDatabase.GameStartClip);
                    }
                })
                .AddTo(this);
        }
    }
}
