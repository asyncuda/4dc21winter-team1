using Character;
using Character.Status;
using UnityEngine;
using Zenject;

namespace Enemy.SoftMochi {
    /// <summary>
    /// 柔らかい餅の情報をまとめるクラス
    /// </summary>
    public class SoftMochiMediator : MonoBehaviour {
        [Inject] private StatusDatabase statusDatabase = default;
        public int Health;
        public int Power { get; private set; }

        private void Start() {
            var status = statusDatabase.GetData(CharacterType.SoftMochi);
            Health = status.Health;
            Power = status.Power;
        }
    }
}
