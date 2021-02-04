namespace Enemy {
    /// <summary>
    /// 斬撃でダメージを受ける
    /// </summary>
    public interface IReceivableSlash {
        void ReceiveDamage(int point);
    }
}
