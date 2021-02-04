using Character;
using Character.Status;
using UnityEngine;
using Zenject;

namespace Enemy.Boss {
    /// <summary>
    /// Bossの情報をまとめるクラス
    /// </summary>
    public class BossMediator : MonoBehaviour {
        [Inject] private StatusDatabase database = default;
        public int Health;
        public int Power { get; private set; }

        private void Start() {
            var status = database.GetData(CharacterType.Boss);
            Health = status.Health;
            Power = status.Power;
        }
    }
}
