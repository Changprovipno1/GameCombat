using System;
namespace Assets.Script
{
    public class PlayerData
    {
        // field
        private int _maxHp;
        private float _attackRange;
        private float _attackCooldown;

        // property        
        public int MaxHp
        {
            get => _maxHp;
            private set => _maxHp = Math.Max(1, value);
        }
        public float AttackRange
        {
            get => _attackRange;
            private set => _attackRange = Math.Max(1, value);
        }
        public float AttackCooldown
        {
            get => _attackCooldown;
            private set => _attackCooldown = Math.Max(1, value);

        }
        public PlayerData(int maxHp, float attackRange, float attackCoolDown)
        {
            MaxHp = maxHp;
            AttackCooldown = attackCoolDown;
            AttackRange = attackRange;
        }
    }
}
