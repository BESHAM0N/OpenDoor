using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _moveSpeed = 35.0f;
    [SerializeField] private float _smoothTime = 1f;

    private bool _isMoving;
    Vector3 _moveDirection;
    private Vector3 _currentVelocity = Vector3.zero;

    private void Start()
    {
        _inputManager.MovementInputReceived += OnMove;
        _inputManager.MovementInputCanceled += OnMoveCanceled;
    }

    private void Update()
    {
        if (_isMoving)      
            MovePlayer();        
    }

    private void OnMove(Vector2 delta)
    {
        _isMoving = true;
        _moveDirection = new Vector3(delta.x, 0, delta.y);
    }

    private void OnMoveCanceled()
    {
        _isMoving = false;
        _moveDirection = Vector2.zero;
    }

    private void MovePlayer()
    {
        _moveDirection.y = 0;

        if (_moveDirection != Vector3.zero)
        {           
            Vector3 cameraForward = _cameraTransform.forward;
            Vector3 cameraRight = _cameraTransform.right;           
            cameraForward.y = 0f;
            cameraRight.y = 0f;           
            cameraForward.Normalize();
            cameraRight.Normalize();           
            Vector3 desiredMoveDirection = (cameraForward * _moveDirection.z) + (cameraRight * _moveDirection.x);           
            Vector3 targetPosition = transform.position + desiredMoveDirection * _moveSpeed * Time.deltaTime;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, _smoothTime);
        }
    }

    private void OnDisable()
    {
        _inputManager.MovementInputReceived -= OnMove;
        _inputManager.MovementInputCanceled -= OnMoveCanceled;
    }
}
