using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Library {
    public class ScreenFader : MonoBehaviour {
        private readonly Rect size = new Rect(0, 0, Screen.width, Screen.height);
        private Color fadeColor = Color.black;
        private float fadeTime;
        private float alpha;
        private bool isFading;

        /// <summary>
        /// 黒幕を生成
        /// </summary>
        public void OnGUI() {
            if (!isFading) return;
            fadeColor.a = alpha;
            GUI.color = fadeColor;
            GUI.DrawTexture(size, Texture2D.whiteTexture);
        }

        public async UniTask FadeOutAsync(float interval) {
            isFading = true;
            fadeTime = 0.0f;
            while (fadeTime <= interval) {
                alpha = Mathf.Lerp(0f, 1f, fadeTime / interval);
                fadeTime += Time.deltaTime;
                await UniTask.Yield();
            }
        }

        public async UniTask FadeInAsync(float interval) {
            var time = 0.0f;
            while (time <= interval) {
                alpha = Mathf.Lerp(1f, 0f, time / interval);
                time += Time.deltaTime;
                await UniTask.Yield();
            }
            isFading = false;
        }
    }
}
