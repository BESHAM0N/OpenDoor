using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput
{
    public event Action<Vector2> MovementInputReceived;
    public event Action MovementInputCanceled;
    private readonly InputMap _inputMap;

    public PlayerInput(InputMap inputMap)
    {
        _inputMap = inputMap;
        _inputMap.PlayerMovement.Move.performed += OnMove;
        _inputMap.PlayerMovement.Move.canceled += OnMove;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        MovementInputReceived?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnMoveCanceled()
    {
        MovementInputCanceled?.Invoke();
    }

    private void OnDisable()
    {
        _inputMap.PlayerMovement.Move.performed -= OnMove;
        _inputMap.PlayerMovement.Move.canceled -= OnMove;
    }
}

