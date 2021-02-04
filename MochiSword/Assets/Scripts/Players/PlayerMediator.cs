using Character;
using Character.Status;
using UnityEngine;
using Zenject;

namespace Players {
    /// <summary>
    /// プレイヤーの情報をまとめるクラス
    /// </summary>
    public class PlayerMediator : MonoBehaviour {
        [Inject] private StatusDatabase database;
        public int Health;
        public int power { get; private set; }
        public int SpecialPoint;

        private void Start() {
            var status = database.GetData(CharacterType.Player) as PlayerData;
            if (status == null) return;
            Health = status.Health;
            power = status.Power;
        }
    }
}
