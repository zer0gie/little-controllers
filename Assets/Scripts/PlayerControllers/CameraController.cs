using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraPivot;

    private Camera _mainCamera;
    
    private float _mouseSensitivity = 0.05f;
    private float _xRotation;

    private void Awake()
    {
        _mainCamera = Camera.main;

        if (_mainCamera != null && cameraPivot != null)
        {
            _mainCamera.transform.SetParent(cameraPivot);
            _mainCamera.transform.localPosition = Vector3.zero;
            _mainCamera.transform.localRotation = Quaternion.identity;
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        HandleCamera();
    }

    private void HandleCamera()
    {
        var inputLook = InputManager.Instance.GetLookDelta();

        var _mouseX = inputLook.x * _mouseSensitivity;
        var _mouseY = inputLook.y * _mouseSensitivity;

        _xRotation -= _mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -89f, 89f);

        cameraPivot.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * _mouseX);
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
