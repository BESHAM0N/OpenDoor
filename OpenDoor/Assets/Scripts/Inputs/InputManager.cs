using System;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public event Action<Vector2> RotationInputReceived;
    public event Action<Vector2> MovementInputReceived;
    public event Action MovementInputCanceled;
    public event Action TakeInputCanceled;
    public event Action DropInputCanceled;

    private InputMap _inputMap;
    private CameraInput _cameraInput;
    private PlayerInput _playerInput;
    private PlayerActions _playerActions;

    private void Awake()
    {
        _inputMap = new InputMap();
        _inputMap.Enable();
        InitLookInput(_inputMap);
        InitMoveInput(_inputMap);
        InitActionInput(_inputMap);
    }

    private void InitLookInput(InputMap map)
    {
        _cameraInput = new CameraInput(map);
        _cameraInput.RotationInputReceived += OnRotationInputReceived;
    }

    private void OnRotationInputReceived(Vector2 delta)
    {
        RotationInputReceived?.Invoke(delta);
    }

    private void InitMoveInput(InputMap map)
    {
        _playerInput = new PlayerInput(map);
        _playerInput.MovementInputCanceled += OnMovementInputCanceled;
        _playerInput.MovementInputReceived += OnMovementInputReceived;
    }

    private void OnMovementInputReceived(Vector2 delta)
    {
        MovementInputReceived?.Invoke(delta);
    }

    private void OnMovementInputCanceled()
    {
        MovementInputCanceled?.Invoke();
    }

    private void InitActionInput(InputMap map)
    {
        _playerActions = new PlayerActions(map);
        _playerActions.TakeInputReceived += OnTakeInputCanceled;
        _playerActions.PutInputReceived += OnDropInputCanceled;
    }

    private void OnTakeInputCanceled()
    {
        TakeInputCanceled?.Invoke();
    }

    private void OnDropInputCanceled()
    {
        DropInputCanceled?.Invoke();
    }

    private void OnDisable()
    {
        _playerInput.MovementInputCanceled -= OnMovementInputCanceled;
        _playerActions.TakeInputReceived -= OnTakeInputCanceled;
        _cameraInput.RotationInputReceived -= OnRotationInputReceived;
        _playerActions.PutInputReceived -= OnDropInputCanceled;
        _playerInput.MovementInputReceived -= OnMovementInputReceived;
    }
}



