using UnityEngine;

namespace Assets.Script
{
    public static class EnemyFactory
    {
        private static int _totalEnemiesCreated = 0;
        public static int TotalEnemiesCreated
        {
            get => _totalEnemiesCreated;
            private set => _totalEnemiesCreated = value;
        }
        public static Enemy Create(Enemy prefab, Vector3 pos)
        {
            if (prefab == null) return null;
            Enemy enemySpawn = Object.Instantiate(prefab, pos, Quaternion.identity);
            TotalEnemiesCreated++;
            Debug.Log($"[Factory] Enemy created. Total: {TotalEnemiesCreated}");
            return enemySpawn;
        }
    }
}
