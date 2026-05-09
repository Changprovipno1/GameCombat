
using System;

namespace Assets.Script
{
    public class WeaponData
    {
        private string _name;
        private int _damage;
        private float _range;
        public string Name
        {
            get => _name;
            private set => _name = value;
        } 
        public int Damage
        {
            get => _damage;
            private set => _damage = Math.Max(1, value);
        }
        public float Range
        {
            get => _range;
            private set => _range = Math.Max(1, value);
        }
        public WeaponData(string name, int damage, float range)
        {
            Name = name;
            Damage = damage;
            Range = range;
        }
    }
}
