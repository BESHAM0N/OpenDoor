using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInput
{
    public event Action<Vector2> RotationInputReceived;
    private readonly InputMap _inputMap;

    public CameraInput(InputMap inputMap)
    {
        _inputMap = inputMap;
        _inputMap.CameraRotation.Look.performed += OnLook;
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        RotationInputReceived?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnDisable()
    {
        _inputMap.CameraRotation.Look.performed -= OnLook;
    }
}

