using Assets.Script;
using UnityEngine;
public class HandleSpawnInput : MonoBehaviour
{
    // SPAWN INPUT
    private const KeyCode SpawnEnemyWaveKey = KeyCode.P;


    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private Enemy _enemy;
    private EnemyData _enemyData;

    void Awake()
    {
        if (!ValidateDependencies())
        {
            enabled = false;
            return;
        }
        _enemyData = new EnemyData("Runner", 100, 20);
    }

    private bool ValidateDependencies()
    {
        if (waveSpawner == null)
        {
            Debug.LogError("WaveSpawner is missing in HandleSpawnInput");
            return false;
        }
        if (_enemy == null)
        {
            Debug.LogError("Enemy is missing in HandleSpawnInput");
            return false;
        }
        return true;
    }

    public void HandleSpawnInputKey()
    {
        if (Input.GetKeyDown(SpawnEnemyWaveKey))
        {
            Debug.Log("P pressed - spawn request");
            waveSpawner.SpawnWave(_enemyData, _enemy);
        }
    }
}
