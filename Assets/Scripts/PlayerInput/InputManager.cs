using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public event EventHandler OnDiaryOpenAction; 
    
    public static InputManager Instance { get; private set; }
    
    private PlayerInputActions _inputActions;

    [SerializeField] private bool MobileInputOn;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        } 
        
        Instance = this;
        _inputActions = new PlayerInputActions();
    }

    private void Start()
    {
        if (Application.isMobilePlatform)
        {
            MobileInputOn = true;
            _inputActions.Player.Enable();
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            MobileInputOn = false;
            _inputActions.Player.Enable();
            Cursor.lockState = CursorLockMode.Locked;
        }
        DeadManager.Instance.OnPlayerDead += DeadManager_OnPlayerDead;
        _inputActions.Player.Diary.performed += DiaryOnPerformed;
    }

    private void DeadManager_OnPlayerDead(object sender, EventArgs e)
    {
        _inputActions.Dispose();
    }

    private void DiaryOnPerformed(InputAction.CallbackContext obj)
    {
        OnDiaryOpenAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        var inputVector = _inputActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }
    public Vector2 GetLookDelta()
    {
        return _inputActions.Player.Look.ReadValue<Vector2>();
    }

    private void OnDestroy()
    {
        _inputActions.Player.Diary.performed -= DiaryOnPerformed;
        _inputActions.Player.Disable();
        _inputActions.Dispose();
    }
}
