
using UnityEngine;

public class HandleInput : MonoBehaviour
{
    private HandleSpawnInput _handleSpawnInput;
    private HandleDebugInput _handleDebugInput;
    private HandleCombatInput _handleCombatInput;
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
        _handleSpawnInput = GetComponent<HandleSpawnInput>();
        _handleDebugInput = GetComponent<HandleDebugInput>();
        _handleCombatInput = GetComponent<HandleCombatInput>();
    }
    private bool ValidateDependencies()
    {
        if (_handleSpawnInput == null)
        {
            Debug.LogError("HandleSpawnInput is missing in HandleInput");
            return false;
        }
        if (_handleDebugInput == null)
        {
            Debug.LogError("HandleDebugInput is missing in HandleInput");
            return false;
        }
        if (_handleCombatInput == null)
        {
            Debug.LogError("HandleCombatInput is missing in HandleInput");
            return false;
        }
        return true;
    }

    public void HandleInputKey()
    {
        _handleSpawnInput.HandleSpawnInputKey();
        _handleDebugInput.HandleDebugInputKey();
        _handleCombatInput.HandleCombatInputKey();
    }

}
