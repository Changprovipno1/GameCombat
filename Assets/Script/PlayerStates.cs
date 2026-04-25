using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    private const float AttackCooldown = 2f;
    private float _lastAttackTime;
    private Player _player;
    private bool _isSilence;
    [SerializeField] private WaveSpawner _waveSpawner;
    private PlayerAttack _playerAttack;

    void Awake()
    {
        _player = GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogWarning("Player is missing");
            enabled = false;
            return;
        }
        _playerAttack = GetComponent<PlayerAttack>();
        if (_playerAttack == null)
        {
            Debug.LogWarning("PlayerAttack is missing");
            enabled = false;
            return;
        }
        if (_waveSpawner == null)
        {
            Debug.LogWarning("WaveSpawner is missing");
            enabled = false;
            return;
        }
    }
    private enum PlayerState
    {
        Idle,
        CombatReady,
        Dead
    }
    private PlayerState CurrentState = PlayerState.Idle;
    public void SyncStateFromPlayer()
    {
        if (_player.IsDead)
        {
            CurrentState = PlayerState.Dead;
        }
    }
    public void HandleStatePlayer()
    {
        switch (CurrentState)
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

    }
    private bool CanCastSkill()
    {
        if (_player.IsDead)
        {
            Debug.Log("Can't cast skill, player is dead");
            return false;
        }
        if (CurrentState != PlayerState.CombatReady)
        {
            Debug.Log("Player is not combat, cannot cast skill");
            return false;
        }
        if (_isSilence)
        {
            Debug.Log("Player is Silence, cannot cast skill");
            return false;
        }
        if (Time.time - _lastAttackTime < AttackCooldown)
        {
            Debug.Log("Player is CoolDown, cannot cast skill");
            return false;
        }
        return true;
    }
    public void InputChangeSilenceEffect()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            _isSilence = !_isSilence;
            Debug.Log($"Silence is {_isSilence} ");
        }

    }
}
