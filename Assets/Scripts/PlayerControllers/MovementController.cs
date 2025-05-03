using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    private CharacterController _chController;
    private Coroutine _hasteBoostRoutine;

    private const float NORMAL_SPEED = 3.5f;
    private const float SPEED_MULTIPLIER = 200f;
    private float _boostedMoveSpeed = 6f;
    private float _currentMoveSpeed;
    
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

    private void Start()
    {
        _currentMoveSpeed = NORMAL_SPEED * SPEED_MULTIPLIER;
    }

    private void Update()
    {
        HandleGravity();
        HandleMovement();
    }

    public void HastePickup(float hasteTime)
    {
        //UIManager.Instance.HasteUIActivate();
        if (_hasteBoostRoutine != null)
        {
            StopCoroutine(_hasteBoostRoutine);
        }
        _hasteBoostRoutine = StartCoroutine(HasteBoost(hasteTime));
    }

    private IEnumerator HasteBoost(float hasteTime)
    {
        _currentMoveSpeed = _boostedMoveSpeed * SPEED_MULTIPLIER;
        Debug.Log("Speed boosted for " + hasteTime + "s");
        yield return new WaitForSeconds(hasteTime);
        _currentMoveSpeed = NORMAL_SPEED * SPEED_MULTIPLIER;
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
        
        var moveDistance = _currentMoveSpeed * Time.deltaTime;
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

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}