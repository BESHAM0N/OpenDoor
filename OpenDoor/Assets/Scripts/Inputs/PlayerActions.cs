using System;
using UnityEngine.InputSystem;

public class PlayerActions 
{
    public event Action TakeInputReceived;
    public event Action PutInputReceived;
    private readonly InputMap _inputMap;

    public PlayerActions(InputMap inputMap)
    {
        _inputMap = inputMap;
        _inputMap.PlayerAction.Interact.performed += OnTake;
        _inputMap.PlayerAction.Drop.performed += OnDrop;
    }

    private void OnTake(InputAction.CallbackContext context)
    {
        TakeInputReceived?.Invoke();
    }

    private void OnDrop(InputAction.CallbackContext context)
    {
        PutInputReceived?.Invoke();
    }

    private void OnDisable()
    {
        _inputMap.PlayerAction.Interact.performed -= OnTake;
        _inputMap.PlayerAction.Drop.performed -= OnDrop;
    }
}
