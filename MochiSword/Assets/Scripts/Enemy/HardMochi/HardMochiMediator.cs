using System;
using Character;

namespace Enemy.HardMochi {
    /// <summary>
    /// 硬い餅の情報をまとめるクラス
    /// </summary>
    public class HardMochiMediator : MediatorBase {
        private void Start() {
            Initialize();
        }

        private void OnDisable() {
            Finalize();
        }
    }
}
