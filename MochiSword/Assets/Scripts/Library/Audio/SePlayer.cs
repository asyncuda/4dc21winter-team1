using UnityEngine;

namespace Library.Audio {
    [RequireComponent(typeof(AudioSource))]
    public class SePlayer : MonoBehaviour {
        private AudioSource audioSource;

        private void Start() {
            audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// SEを1回再生する
        /// </summary>
        public void PlayOneShot(AudioClip clip) {
            audioSource.PlayOneShot(clip);
        }

        /// <summary>
        /// 音量を調整してSEを1回再生する
        /// </summary>
        public void PlayOneShot(AudioClip clip, float volumeScale) {
            audioSource.PlayOneShot(clip, volumeScale);
        }
    }
}
