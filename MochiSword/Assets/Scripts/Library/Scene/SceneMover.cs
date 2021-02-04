using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Library.Scene {
    public static class SceneMover {
        private static readonly ScreenFader ScreenFader;
        private const float FadeTime = 1.0f;
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
            await ScreenFader.FadeOutAsync(FadeTime);
            await SceneManager.LoadSceneAsync((int) scene);
            await ScreenFader.FadeInAsync(FadeTime);
            isSceneChanging = false;
        }
    }
}
