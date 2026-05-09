using UnityEngine;

namespace Assets.Script
{
    public static class DebugOverlay
    {
        public static void LogStatus(int currentHp, int maxHp, bool isDead)
        {
            string status = isDead ? "Dead" : "Alive";
            Debug.Log($"[DEBUG] Hp: {currentHp}/{maxHp}, status: {status}");
        }
        public static void LogEnemyCount(int alive, int dead)
        {
            Debug.Log($"[DEBUG] Count Enemy Alive: {alive}, Count Enemy Dead: {dead}");
        }
        public static void LogCombatStats(int lastDamage, int totalDamage, int totalKill, int totalKillThisSession)
        {
            Debug.Log($"[DEBUG] Enemy takes damage: {lastDamage}/ {totalDamage}");
            Debug.Log($"[DEBUG] Count kill enemy: {totalKill}");
            Debug.Log($"[DEBUG] Count total killed enemies: {totalKillThisSession}");
        }
    }
}
