namespace Enemy {
    /// <summary>
    /// 突き攻撃でダメージを受ける
    /// </summary>
    public interface IReceivableStab {
        void ReceiveDamage(int point);
    }
}
