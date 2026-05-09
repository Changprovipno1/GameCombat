using Assets.Script;
using UnityEngine;

public class HandleDebugInput : MonoBehaviour
{
    // DEBUG INPUT
    private const KeyCode DebugNearestEnemyKey = KeyCode.Z;
    private const KeyCode DebugAllInfoKey = KeyCode.F1;
    private const KeyCode DebugResetSessionKey = KeyCode.F2;

    [SerializeField] private Enemy _enemy;
    private PlayerDamageNearEnemy _playerDamageNear;
    private Player _player;
    [SerializeField] private WaveSpawner _waveSpawner;

    void Awake()
    {
        AssignDependencies();
        if (!ValidateDependencies())
        {
            enabled = false;
            return;
        }
    }
    private void AssignDependencies()
    {
        _playerDamageNear = GetComponent<PlayerDamageNearEnemy>();
        _player = GetComponent<Player>();
    }
    private bool ValidateDependencies()
    {
        if (_enemy == null)
        {
            Debug.LogError("Enemy is missing in HandleDebugInput");
            return false;
        }
        if (_playerDamageNear == null)
        {
            Debug.LogError("PlayerDamageNearEnemy is missing in HandleDebugInput");
            return false;
        }
        if (_player == null)
        {
            Debug.LogError("Player is missing in HandleDebugInput");
            return false;
        }
        if (_waveSpawner == null)
        {
            Debug.LogError("WaveSpawner is missing in HandleDebugInput");
            return false;
        }
        return true;
    }

    public void HandleDebugInputKey()
    {
        if (Input.GetKeyDown(DebugNearestEnemyKey))
        {
            _playerDamageNear.PrintEnemyNearest();
        }

        if (Input.GetKeyDown(DebugResetSessionKey))
        {
            CombatStatTracker.ResetCombatStats();
            CombatStatTracker.ResetSessionStats();
            Debug.Log("[DEBUG] Session reset");
            DebugOverlay.LogCombatStats(CombatStatTracker.LastDamage, CombatStatTracker.TotalDamage, CombatStatTracker.CountKillEnemy, CombatStatTracker.TotalEnemiesKilledThisSession);
        }
        if (Input.GetKeyDown(DebugAllInfoKey))
        {
            _enemy.PrintEnemyInfo();
            _player.PrintPlayerInfo();
            HandleCountEnemy();
            DebugOverlay.LogCombatStats(CombatStatTracker.LastDamage, CombatStatTracker.TotalDamage, CombatStatTracker.CountKillEnemy, CombatStatTracker.TotalEnemiesKilledThisSession);
        }
    }
    private void HandleCountEnemy()
    {
        int aliveEnemy = _waveSpawner.CountEnemiesAlive();
        int deadEnemy = _waveSpawner.CountEnemiesDead();
        DebugOverlay.LogEnemyCount(aliveEnemy, deadEnemy);
    }
}
