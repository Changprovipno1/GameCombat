using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    private PlayerAttack _playerAttack;
    private Player _player;
    [SerializeField] private WaveSpawner waveSpawner;
    private IReadOnlyList<Enemy> enemies;
    [SerializeField] private Enemy _enemy;
    private PlayerStates _playerStates;
    private PlayerDamageNearEnemy _playerDamageNear;


    void Awake()
    {
        _playerStates = GetComponent<PlayerStates>();
        if (_playerStates == null)
        {
            Debug.LogError("PlayerStates is missing");
            enabled = false;
            return;
        }
        if (waveSpawner == null)
        {
            Debug.LogError("WaveSpawner is missing");
            enabled = false;
            return;
        }
        enemies = waveSpawner.Enemies;
        if (enemies == null)
        {
            Debug.LogError("Enemy List is null");
            enabled = false;
            return;
        }
        _playerAttack = GetComponent<PlayerAttack>();
        if (_playerAttack == null)
        {
            Debug.LogError("PlayerAttack is missing");
            enabled = false;
            return;
        }
        _player = GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is missing");
            enabled = false;
            return;
        }
        _playerDamageNear = GetComponent<PlayerDamageNearEnemy>();
        if (_playerDamageNear == null)
        {
            Debug.LogError("PlayerDamageNearEnemy is missing");
            enabled = false;
            return;
        }
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("P pressed - spawn request");
            waveSpawner.SpawnWave(_enemy);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _playerDamageNear.PrintEnemyNearest();
        }
    }
    
    
}
