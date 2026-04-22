using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{

    private enum PlayerState
    {
        Idle,
        CombatReady,
        Dead
    }
    private PlayerState CurrentState = PlayerState.Idle;
    float AttackCooldown = 2f;
    float LastAttackTime ;
    private bool isSilence;
    private PlayerAttack playerAttack;
    private Player player;
    [SerializeField] private WaveSpawner waveSpawner;
    private List<Enemy> enemies;
    public Enemy enemy;
    private void Awake()
    {
        enemies = waveSpawner.Enemies;
        playerAttack = GetComponent<PlayerAttack>();
        player = GetComponent<Player>();
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
        SyncStateFromPlayer();
        HandleInputState();
        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleStatePlayer();
        }
        InputChangeSilenceEffect();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            HandleCastSkill();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            waveSpawner.SpawnWave(enemy);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            playerAttack.PrintEnemyNearest();
        }
    }
    void HandleInputState()
    {
        if (CurrentState == PlayerState.Dead)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            CurrentState = PlayerState.Idle;
            Debug.Log("Player's state is idle");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            CurrentState = PlayerState.CombatReady;
            Debug.Log("Player's state is combat");
        }

    }
    void SyncStateFromPlayer()
    {
        if (player.IsDead)
        {
            CurrentState = PlayerState.Dead;
        }
    }
    void HandleStatePlayer()
    {
        switch (CurrentState)
        {
            case PlayerState.Idle:
                Debug.Log("Player are not ready to fight");
                break;
            case PlayerState.CombatReady:
                
                if (Time.time - LastAttackTime < AttackCooldown)
                {
                    Debug.Log("Player is cooldown");
                    return;
                }
                Debug.Log("Player is attacking");
                LastAttackTime = Time.time;
                break;
            case PlayerState.Dead:
                Debug.Log("Player is dead, cannot interact");
                break;
            default:
                return;
        }
        
    }
    void HandleCastSkill()
    { 
        if (player.IsDead)
        {
            Debug.Log("Can't cast skill, player is dead");
            return;
        }
        if (CurrentState != PlayerState.CombatReady)
        {
            Debug.Log("Player is not combat, cannot cast skill");
            return;
        }
        if (isSilence)
        {
            Debug.Log("Player is Silence, cannot cast skil");
            return;
        }
        playerAttack.AttackAllEnemyInRange();
        Debug.Log("cast skill successful");
        waveSpawner.CleanupDeadEnemy();

    }
    void InputChangeSilenceEffect()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            isSilence = !isSilence;
            Debug.Log($"Silence is {isSilence} ");
        }

    }
}
