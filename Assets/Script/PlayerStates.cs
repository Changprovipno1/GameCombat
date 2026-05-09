using Assets.Script;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    private float AttackCooldown => _player.AttackCooldown;
    private float _lastAttackTime;
    private Player _player;
    private bool _isSilence;
    private PlayerAttack _playerAttack;
    [SerializeField] private WaveSpawner _waveSpawner;
    private enum PlayerState
    {
        Idle,
        CombatReady,
        Dead
    }
    private PlayerState _currentState = PlayerState.Idle;
    private static float _globalCooldownMultiplier;

    public static float GlobalCooldownMultiplier
    {
        get => _globalCooldownMultiplier;
        private set => _globalCooldownMultiplier = 1f;
    }
    void Awake()
    {
        AssignDependencies();
        if (!ValidateDependencies())
        {
            enabled = false;
            return;
        }
    }
    private bool ValidateDependencies()
    {
        if (_waveSpawner == null)
        {
            Debug.LogError("WaveSpawner is missing in PlayerStates");
            return false;
        }     
        if (_player == null)
        {
            Debug.LogError("Player is missing in PlayerStates");
            return false;
        }
        
        if (_playerAttack == null)
        {
            Debug.LogError("PlayerAttack is missing in PlayerStates");
            return false;
        }
        return true;
    }
    private void AssignDependencies()
    {
        _player = GetComponent<Player>();
        _playerAttack = GetComponent<PlayerAttack>();
    }

    public void SyncStateFromPlayer()
    {
        if (_player.IsDead)
        {
            _currentState = PlayerState.Dead;
        }
    }
    public void ToggleStatePlayer()
    {
        switch (_currentState)
        {
            case PlayerState.Idle:
                Debug.Log("Player are not ready to fight");
                break;
            case PlayerState.CombatReady:
                Debug.Log("Player is attacking");
                break;
            case PlayerState.Dead:
                Debug.Log("Player is dead, cannot interact");
                break;
            default:
                return;
        }

    }
    public void HandleInputState()
    {
        if (_currentState == PlayerState.Dead)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            _currentState = PlayerState.Idle;
            Debug.Log("Player's state is idle");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            _currentState = PlayerState.CombatReady;
            Debug.Log("Player's state is combat");
        }

    }

    public void TestKillEnemy()
    {
        bool hasHitNearEnemy = _playerAttack.AttackAllEnemyInRange();

        if (!hasHitNearEnemy)
        {
            Debug.Log("No enemy in range, skill failed");
            return;
        }
        _waveSpawner.CleanupDeadEnemy();
        _waveSpawner.PrintList();
    }

    public void HandleCastSkill()
    {   if (!CanCastSkill()) return;
        bool hasHitNearEnemy = _playerAttack.AttackAllEnemyInRange();
       
        if (!hasHitNearEnemy)
        {
            Debug.Log("No enemy in range, skill failed");
            return;
        }
        _lastAttackTime = Time.time;
        _waveSpawner.CleanupDeadEnemy();
        _waveSpawner.PrintList();
    }
    private bool CanCastSkill()
    {
        float _effectiveCooldown = AttackCooldown * GlobalCooldownMultiplier;
        if (_player.IsDead)
        {
            Debug.Log("Can't cast skill, player is dead");
            return false;
        }
        if (_currentState != PlayerState.CombatReady)
        {
            Debug.Log("Player is not combat, cannot cast skill");
            return false;
        }
        if (_isSilence)
        {
            Debug.Log("Player is Silence, cannot cast skill");
            return false;
        }
        if (Time.time - _lastAttackTime < _effectiveCooldown )
        {
            Debug.Log("Player is CoolDown, cannot cast skill");
            return false;
        }
        return true;
    }
    public void InputChangeSilenceEffect()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            _isSilence = !_isSilence;
            Debug.Log($"Silence is {_isSilence} ");
        }

    }

    //const k thuộc instance, cũng k thuộc về class, nó giống static 
    public void ToggleMultiplier()
    {
        if (GlobalCooldownMultiplier == 1f)
        {
            GlobalCooldownMultiplier = 0.5f;
            Debug.Log("GlobalCooldownMultiplier = 0.5f");
        }
        else
        {
            GlobalCooldownMultiplier = 1f;
            Debug.Log("GlobalCooldownMultiplier = 1f");
        }
    }

}
