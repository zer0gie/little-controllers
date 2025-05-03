using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
        [SerializeField] private RectTransform background;
        [SerializeField] private RectTransform handle;
        [SerializeField] private InputAxis joystickAxe;

        private Canvas _canvas;
        private Vector2 _input;
        
        public enum InputAxis
        {
            Camera,
            Movement
        }

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            background.gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            background.position = eventData.position;
            background.gameObject.SetActive(true);
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                background, eventData.position, _canvas.worldCamera, out Vector2 localPoint);

            var radius = background.sizeDelta * 0.5f;
            _input = Vector2.ClampMagnitude(localPoint / radius, 1f);

            handle.anchoredPosition = _input * radius;

            if (joystickAxe == InputAxis.Movement)
            {
                InputManager.Instance.SetMobileMovementVector(_input);
            }
            else if (joystickAxe == InputAxis.Camera)
            {
                InputManager.Instance.SetMobileCameraVector(_input);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _input = Vector2.zero;
            handle.anchoredPosition = Vector2.zero;
            background.gameObject.SetActive(false);
            if (joystickAxe == InputAxis.Movement)
            {
                InputManager.Instance.SetMobileMovementVector(Vector2.zero);
            }
            else if (joystickAxe == InputAxis.Camera)
            {
                InputManager.Instance.SetMobileCameraVector(Vector2.zero);
            }
        }
}