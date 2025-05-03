using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject mobileUI;
    [SerializeField] private InputMode currentInput;
    public event EventHandler OnDiaryOpenAction;

    public static InputManager Instance { get; private set; }

    private PlayerInputActions _inputActions;
    private Vector2 _mobileInputMoveVector;
    private Vector2 _mobileInputCameraVector;
    private float _mobileCameraMultiplier = 25f;


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
            currentInput = InputMode.Mobile;
            _inputActions.Player.Enable();
            Cursor.lockState = CursorLockMode.None;
            mobileUI.gameObject.SetActive(true);
        }
        else
        {
            currentInput = InputMode.Default;
            _inputActions.Player.Enable();
            Cursor.lockState = CursorLockMode.Locked;
            mobileUI.gameObject.SetActive(false);
        }

        DeadManager.Instance.OnPlayerDead += DeadManager_OnPlayerDead;
        _inputActions.Player.Diary.performed += DiaryOnPerformed;
    }

    private void Update()
    {
        if (currentInput == InputMode.Mobile && !mobileUI.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            mobileUI.SetActive(true);
        }
        if (currentInput == InputMode.Default && mobileUI.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            mobileUI.SetActive(false);
        }
    }

    public void SetMobileMovementVector(Vector2 dir)
    {
        _mobileInputMoveVector = dir;
    }

    public void SetMobileCameraVector(Vector2 dir)
    {
        _mobileInputCameraVector = dir;
    }

    private enum InputMode
    {
        Mobile,
        Default
    }

    private void DeadManager_OnPlayerDead(object sender, EventArgs e)
    {
        _inputActions.Disable();
    }

    private void DiaryOnPerformed(InputAction.CallbackContext obj)
    {
        OnDiaryOpenAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        if (currentInput == InputMode.Mobile)
        {
            return _mobileInputMoveVector.normalized;
        }
        else
        {
            var inputVector = _inputActions.Player.Move.ReadValue<Vector2>();
            return inputVector.normalized;
        }
    }

    public Vector2 GetLookDelta()
    {
        if (currentInput == InputMode.Mobile)
        {
            return _mobileInputCameraVector.normalized * _mobileCameraMultiplier;
        }
        else
        {
            return _inputActions.Player.Look.ReadValue<Vector2>();
        }
    }

    private void OnDestroy()
    {
        _inputActions.Player.Diary.performed -= DiaryOnPerformed;
        DeadManager.Instance.OnPlayerDead -= DeadManager_OnPlayerDead;
        _inputActions.Player.Disable();
        _inputActions.Dispose();
    }
}