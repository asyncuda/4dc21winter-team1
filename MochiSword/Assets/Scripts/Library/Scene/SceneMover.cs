using Cysharp.Threading.Tasks;
using Library.Pause;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Library.Scene {
    public static class SceneMover {
        private static readonly ScreenFader ScreenFader;
        private const float FadeTime = 1.0f;
        private const float RestartTime = 0.2f;
        private static bool isSceneChanging;

        static SceneMover() {
            ScreenFader = new GameObject(nameof(Scene.ScreenFader)).AddComponent<ScreenFader>();
            Object.DontDestroyOnLoad(ScreenFader.gameObject);
        }
        
        /// <summary>
        /// シーン遷移する
        /// TODO: cancellationToken
        /// </summary>
        public static async UniTaskVoid MoveAsync(Scenes scene) {
            if (isSceneChanging) return;
            isSceneChanging = true;
            Pauser.Clear();
            await ScreenFader.FadeOutAsync(FadeTime);
            await SceneManager.LoadSceneAsync((int) scene);
            await ScreenFader.FadeInAsync(FadeTime);
            isSceneChanging = false;
        }

        /// <summary>
        /// リスタートする
        /// </summary>
        public static void Restart() {
            Move(Scenes.Game, RestartTime).Forget();
        }

        private static async UniTaskVoid Move(Scenes scene, float time) {
            if (isSceneChanging) return;
            isSceneChanging = true;
            await ScreenFader.FadeOutAsync(time);
            await SceneManager.LoadSceneAsync((int) scene);
            await ScreenFader.FadeInAsync(time);
            isSceneChanging = false;
        }
    }
}
