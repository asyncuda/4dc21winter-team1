using Character;
using Character.Status;
using UnityEngine;
using Zenject;

namespace Enemy.HardMochi {
    /// <summary>
    /// 硬い餅の情報をまとめるクラス
    /// </summary>
    public class HardMochiMediator : MonoBehaviour {
        [Inject] private StatusDatabase statusDatabase = default;
        public int Health;
        public int Power { get; private set; }

        private void Start() {
            var status = statusDatabase.GetData(CharacterType.HardMochi);
            Health = status.Health;
            Power = status.Power;
        }
    }
}
