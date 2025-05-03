using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraPivot;

    private Camera _mainCamera;
    
    private float _sensitivity = 0.05f;
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
    private void LateUpdate()
    {
        HandleCamera();
    }

    private void HandleCamera()
    {
        var inputLook = InputManager.Instance.GetLookDelta();

        var x = inputLook.x * _sensitivity;
        var y = inputLook.y * _sensitivity;

        _xRotation -= y;
        _xRotation = Mathf.Clamp(_xRotation, -89f, 89f);

        cameraPivot.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * x);
    }
}
