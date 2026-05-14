using System;

namespace Assets.Script
{
    public class CharacterHealthSystemBase
    {
        // field
        private int _currentHp;
        private readonly int _maxHp;

        //property
        public int CurrentHp
        {
            get => _currentHp;
            private set => _currentHp = Math.Min(MaxHp, Math.Max(0, value));
        }
        public int MaxHp => _maxHp;
        public bool IsDead => CurrentHp == 0;
        public float HpRatio => (float)CurrentHp / MaxHp;

        // magic number
        private const int MinimumDamage = 0;

        public CharacterHealthSystemBase(int maxHp)
        {
            _maxHp = maxHp <= 0 ? 1 : maxHp;
            CurrentHp = _maxHp;
        }

        public virtual void TakeDamage(int rawDamage)
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
