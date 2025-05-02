using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    private CharacterController _chController;

    private float _moveSpeed = 200f;
    private readonly float _gravity = -13f;
    private readonly float _smoothTime = 0.05f;
    private readonly LayerMask _floorLayer = 1 << 7;

    private Vector3 _fallVelocity;
    private Vector3 _currentVelocity;
    private Vector3 _smoothDumpVelocity;

    private Vector3 _floorCheckDistance;

    private void Awake()
    {
        _chController = gameObject.GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        HandleGravity();
        HandleMovement();
    }

    private void HandleMovement()
    {
        var inputVector = InputManager.Instance.GetMovementVectorNormalized();
        Vector3 moveDirection = new(inputVector.x, 0f, inputVector.y);
        moveDirection = transform.TransformDirection(moveDirection);

        if (!PredictFloor(moveDirection))
        {
            moveDirection = Vector3.zero;
        }
        
        var moveDistance = _moveSpeed * Time.deltaTime;
        var targetVelocity = moveDirection * moveDistance;
        _currentVelocity = Vector3.SmoothDamp(_currentVelocity, targetVelocity, ref _smoothDumpVelocity, _smoothTime);

        _chController.Move(_currentVelocity * Time.deltaTime);
    }

    private bool PredictFloor(Vector3 moveDir) 
    { 
        var predictedPos = transform.position + moveDir;
        var rayDistance = 2f;
        return Physics.Raycast(predictedPos, Vector3.down, rayDistance, _floorLayer);
    }
    private void HandleGravity()
    { 
        if (_chController.isGrounded && _fallVelocity.y < 0)
        {
            _fallVelocity.y = -2f;
        }

        _fallVelocity.y += _gravity * Time.fixedDeltaTime;

        _chController.Move(_fallVelocity * Time.deltaTime);
    }
}