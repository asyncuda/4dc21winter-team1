using UnityEngine;

namespace Library.Effects {
    public class EffectPlayer : MonoBehaviour {
        /// <summary>
        /// 指定した場所にエフェクトを生成する
        /// </summary>
        public void Play(GameObject effect, Vector2 position) {
            Instantiate(effect, position, Quaternion.identity);
        }
    }
}
