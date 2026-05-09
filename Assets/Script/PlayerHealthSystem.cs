using System;

namespace Assets.Script
{
    public class PlayerHealthSystem
    {
        private readonly int _maxHP;
        private int _currentHp;

        // property
        public int MaxHp => _maxHP;
        public int CurrentHp
        {
            get => _currentHp;
            private set => _currentHp = Math.Min(MaxHp,Math.Max(0, value));
        }
        public bool IsDead => CurrentHp == 0;

        // magic number
        private const int MinimumDamage = 0;

        public PlayerHealthSystem(int maxHp)
        {
            _maxHP = maxHp <= 0 ? 1 : maxHp;
            CurrentHp = _maxHP;
        }

        public void TakeDamage(int rawDamage)
        {
            if (rawDamage <= MinimumDamage || IsDead) return;
            ApplyDamage(rawDamage);
        }

        private void ApplyDamage(int incomingDamage)
        {
            CurrentHp -= incomingDamage;
        }

        public void Heal(int amountHp)
        {
            if (IsDead || amountHp <= 0) return;
            CurrentHp += amountHp;
        }
    }
}
