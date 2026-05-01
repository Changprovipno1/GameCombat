namespace Assets.Script
{
    public class EnemyData
    {
        public readonly int MaxHp;
        public readonly string EnemyName;
        public readonly int Damage;

        public EnemyData(string enemyName, int maxHp, int damage)
        {
            MaxHp = maxHp;
            EnemyName = enemyName;
            Damage = damage;
        }
    }
}
