using Assets.Script;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    private HandleInput _handleInput;

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
        _handleInput = GetComponent<HandleInput>();
    }
    private bool ValidateDependencies()
    {
        if (_handleInput == null)
        {
            Debug.LogError("HandleSpawnInput is missing in PlayerActionController");
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
        _handleInput.HandleInputKey();
    }
    
}
