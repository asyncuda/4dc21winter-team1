using Character;

namespace Enemy.SoftMochi {
    /// <summary>
    /// 柔らかい餅の情報をまとめるクラス
    /// </summary>
    public class SoftMochiMediator : MediatorBase{
        private void Start() {
            Initialize();
        }

        private void OnDisable() {
            Finalize();
        }
    }
}
