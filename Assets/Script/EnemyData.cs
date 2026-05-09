using System;
namespace Assets.Script
{
    public class EnemyData
    {
        // field 
        private int _maxHp;
        private string _enemyName;
        private int _damage;

        //property
        public int MaxHp
        {
            get => _maxHp;
            private set => _maxHp = Math.Max(1, value);
        }
        public string EnemyName
        {
            get => _enemyName;
            private set => _enemyName = value;
        }
        public int Damage
        {
            get => _damage;
            private set => _damage = Math.Max(1, value);
        }

        public EnemyData(string enemyName, int maxHp, int damage)
        {
            MaxHp = maxHp;
            EnemyName = enemyName;
            Damage = damage;
        }
        
    }
}
