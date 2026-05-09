
namespace Assets.Script
{
    public static class CombatStatTracker
    {
        // damage enemy takes
        private static int _lastDamage = 0 ;
        // raw damage
        private static int _totalDamage = 0;
        // count kill
        private static int _countKillEnemy = 0;
        // đặt TotalEnemiesKilledThisSession vì đây là class theo dõi chương trình, tồn tại tới cuối chương trình
        private static int _totalEnemiesKilledThisSession = 0;

        // property
        public static int LastDamage
        {
            get => _lastDamage;
            private set => _lastDamage = value;
        }
        public static int TotalDamage
        {
            get => _totalDamage;
            private set => _totalDamage = value;
        }
        public static int CountKillEnemy
        {
            get => _countKillEnemy;
            private set => _countKillEnemy = value;
        }
        public static int TotalEnemiesKilledThisSession
        {
            get => _totalEnemiesKilledThisSession;
            private set => _totalEnemiesKilledThisSession = value;
        }
        
        // method
        public static void RecordDamage(int inputDamage)
        {
            if (inputDamage <= 0) return;
            LastDamage = inputDamage;
            TotalDamage += inputDamage;
        }
        public static void RecordKillWave()
        {
            CountKillEnemy++;
        }
        public static void RecordKillThisSession()
        {
            TotalEnemiesKilledThisSession++;
        }

        public static void ResetSessionStats()
        {
            TotalEnemiesKilledThisSession = 0;
        }
        public static void ResetCombatStats()
        {
            LastDamage = 0;
            TotalDamage = 0;
            CountKillEnemy = 0;
        }
    }
}
