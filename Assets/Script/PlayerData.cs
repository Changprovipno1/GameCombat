using System;
namespace Assets.Script
{
    public class PlayerData
    {
        public readonly int MaxHp;
        public readonly float AttackRange;
        public readonly float AttackCooldown;
        public PlayerData(int maxHp, float attackRange, float attackCoolDown)
        {
            if (maxHp <= 0)
            {
                Console.WriteLine($"MaxHp invalid ({maxHp}), clamped to 1");
                maxHp = 1;
            }
            if (attackRange <= 0)
            {
                Console.WriteLine($"AttackRange invalid ({attackRange}), clamped to 1");
                attackRange = 1;
            }
            if (attackCoolDown <= 0)
            {
                Console.WriteLine($"AttackCooldown invalid ({attackCoolDown}), clamped to 0.1f");
                attackCoolDown = 0.1f;
            }
            MaxHp = maxHp;
            AttackCooldown = attackCoolDown;
            AttackRange = attackRange;
        }
    }
}
