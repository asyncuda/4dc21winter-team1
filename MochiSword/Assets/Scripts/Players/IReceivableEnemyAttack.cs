namespace Players {
    /// <summary>
    /// 敵の攻撃を受ける
    /// </summary>
    public interface IReceivableEnemyAttack {
        void ReceiveDamage(int point);
    }
}
