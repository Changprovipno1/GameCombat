using System;
namespace Assets.Script
{
    public class EliteEnemyHealthSystem : EnemyHealthSystem
    {
        private int _armor;
        public int Armor
        {
            get => _armor;
            private set => _armor = Math.Max(0, value);
        }
        public EliteEnemyHealthSystem(int maxHp, int armor) : base(maxHp)
        {
            Armor = armor;
        }
        public override void TakeDamage(int rawDamage)
        {
            if (rawDamage <= 0 || IsDead) return;
            int finalDamage = Math.Max(0, (rawDamage - Armor));
            Console.WriteLine($"Damage: { finalDamage}");
            base.TakeDamage(finalDamage);
        }
    }
}
