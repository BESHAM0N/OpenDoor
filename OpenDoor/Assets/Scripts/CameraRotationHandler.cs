using UnityEngine;

public class CameraRotationHandler : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;

    private float _sensitivity = 10f;
    private float _minVerticalAngle = -30;
    private float _maxVerticalAngle = 30;
    private float _currentVerticalAngle = 0f;
    private float _currentHorizontalAngle = 0f;
    private float _initialHorizontalAngle;
    private float _initialVerticalAngle;

    private void Start()
    {
        _initialHorizontalAngle = transform.eulerAngles.y;
        _initialVerticalAngle = transform.eulerAngles.x;

        _currentHorizontalAngle = _initialHorizontalAngle;
        _currentVerticalAngle = _initialVerticalAngle;
        _inputManager.RotationInputReceived += OnLook;       
    }  

    private void OnLook(Vector2 delta)
    {
        var deltaTime = Time.deltaTime;
        _currentVerticalAngle -= _sensitivity * delta.y * deltaTime;
        _currentHorizontalAngle += _sensitivity * delta.x * deltaTime;
        _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle, _minVerticalAngle, _maxVerticalAngle);
        transform.rotation = Quaternion.Euler(_currentVerticalAngle, _currentHorizontalAngle, 0);
    }

    private void OnDisable()
    {
        _inputManager.RotationInputReceived -= OnLook;
    }
}

