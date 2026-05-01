
namespace Assets.Script
{
    public class WeaponData
    {
        public readonly string Name;
        public readonly int Damage;
        public readonly float Range;
        public WeaponData(string name, int damage, float range)
        {
            Name = name;
            Damage = damage;
            Range = range;
        }
    }
}
