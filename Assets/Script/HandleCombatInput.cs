using UnityEngine;

public class HandleCombatInput : MonoBehaviour
{
    // COMBAT INPUT
    private const KeyCode AttackKey = KeyCode.Q;
    private const KeyCode ToggleCombatStateKey = KeyCode.E;
    private const KeyCode ToggleCooldownMultiplierKey = KeyCode.C;
    private PlayerStates _playerStates;

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
        _playerStates = GetComponent<PlayerStates>();
    }
    private bool ValidateDependencies()
    {
        if (_playerStates == null)
        {
            Debug.LogError("PlayerStates is missing in HandleCombatInput");
            return false;
        }
        return true;
    }


    public void HandleCombatInputKey()
    {
        _playerStates.SyncStateFromPlayer();
        _playerStates.HandleInputState();
        if (Input.GetKeyDown(ToggleCombatStateKey))
        {
            _playerStates.LogStatePlayer();
        }

        _playerStates.InputChangeSilenceEffect();

        if (Input.GetKeyDown(AttackKey))
        {
            _playerStates.HandleCastSkill();
            //_playerStates.TestKillEnemy();
        }
        if (Input.GetKeyDown(ToggleCooldownMultiplierKey))
        {
            _playerStates.ToggleMultiplier();
        }
    }
}
