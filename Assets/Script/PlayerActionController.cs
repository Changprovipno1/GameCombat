using Assets.Script;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    private PlayerAttack _playerAttack;
    private Player _player;
    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private Enemy _enemy;
    private PlayerStates _playerStates;
    private PlayerDamageNearEnemy _playerDamageNear;
    private EnemyData _enemyData;

    void Awake()
    {
        AssignDependencies();
        if (!ValidateDependencies())
        {
            enabled = false;
            return;
        }
        _enemyData = new EnemyData("Runner", 100, 20);
    }
    private void AssignDependencies()
    {
        _playerStates = GetComponent<PlayerStates>();
        _playerAttack = GetComponent<PlayerAttack>();
        _player = GetComponent<Player>();
        _playerDamageNear = GetComponent<PlayerDamageNearEnemy>();
    }
    private bool ValidateDependencies()
    {
        if (_playerStates == null)
        {
            Debug.LogError("PlayerStates is missing");
            return false;
        }
        if (waveSpawner == null)
        {
            Debug.LogError("WaveSpawner is missing");
            return false;
        }

        if (_playerAttack == null)
        {
            Debug.LogError("PlayerAttack is missing");
            return false;
        }

        if (_player == null)
        {
            Debug.LogError("Player is missing");
            return false;
        }

        if (_playerDamageNear == null)
        {
            Debug.LogError("PlayerDamageNearEnemy is missing");
            return false;
        }
        return true;
    }

    void Start()
    {
        Debug.Log("Input Player State (1 - Idle, 2 -  Combat): ");
    }
    void Update()
    {

        HandleInput();
        
            
    }
    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("P pressed - spawn request");
            waveSpawner.SpawnWave(_enemyData, _enemy);
        }
        _playerStates.SyncStateFromPlayer();
        _playerStates.HandleInputState();
        if (Input.GetKeyDown(KeyCode.E))
        {
            _playerStates.HandleStatePlayer();
        }
        _playerStates.InputChangeSilenceEffect();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _playerStates.HandleCastSkill();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            _playerDamageNear.PrintEnemyNearest();
        }
    }
    
    
}
