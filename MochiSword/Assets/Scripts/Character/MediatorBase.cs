using Library.Pause;
using UnityEngine;

namespace Character {
    public class MediatorBase : MonoBehaviour, IPausable {
        private Behaviour behaviour;
        private Rigidbody2D physicsEngine;

        public void Pause() {
            behaviour.enabled = false;
            physicsEngine.simulated = false;
        }

        public void Resume() {
            behaviour.enabled = true;
            physicsEngine.simulated = true;
        }

        protected void Initialize() {
            Pauser.AddList(this);
            behaviour = GetComponent<Behaviour>();
            physicsEngine = GetComponent<Rigidbody2D>();
        }

        protected void Finalize() {
            Pauser.RemoveList(this);
        }
    }
}
