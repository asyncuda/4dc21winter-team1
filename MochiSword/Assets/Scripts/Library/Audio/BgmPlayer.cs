using UnityEngine;

namespace Library.Audio {
    public class BgmPlayer : MonoBehaviour {
        private AudioSource audioSource;

        private void Start() {
            audioSource = GetComponent<AudioSource>();
        }

        public void Play(AudioClip clip) {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
