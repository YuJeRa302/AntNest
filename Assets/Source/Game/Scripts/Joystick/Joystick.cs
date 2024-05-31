using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [Header("[JoystickBackgorund;]")]
    [SerializeField] private Image _joystickBackgorund;
    [Header("[Joystick]")]
    [SerializeField] private Image _joystick;

    private readonly string _axisHorizontal = "Horizontal";
    private readonly string _axisVertical = "Vertical";
    private readonly int _nullValue = 0;
    private readonly int _multiplier = 2;
    private readonly float _defaultValueMagnitude = 1.0f;

    private Vector2 _inputVector;

    public virtual void OnPointerDown(PointerEventData pointerEventData)
    {
        OnDrag(pointerEventData);
    }

    public virtual void OnPointerUp(PointerEventData pointerEventData)
    {
        _inputVector = Vector2.zero;
        _joystick.rectTransform.anchoredPosition = Vector2.zero;
    }

    public virtual void OnDrag(PointerEventData pointerEventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackgorund.rectTransform, pointerEventData.position, pointerEventData.pressEventCamera, out Vector2 position))
        {
            position.x /= _joystickBackgorund.rectTransform.sizeDelta.x;
            position.y /= _joystickBackgorund.rectTransform.sizeDelta.y;
            _inputVector = new Vector2(position.x * _multiplier, position.y * _multiplier);
            _inputVector = (_inputVector.magnitude > _defaultValueMagnitude) ? _inputVector.normalized : _inputVector;
            _joystick.rectTransform.anchoredPosition = new Vector2(_inputVector.x * (_joystickBackgorund.rectTransform.sizeDelta.x / _multiplier), _inputVector.y * (_joystickBackgorund.rectTransform.sizeDelta.y / _multiplier));
        }
    }

    public float GetHorizontalValue()
    {
        return _inputVector.x != _nullValue ? _inputVector.x : Input.GetAxis(_axisHorizontal);
    }

    public float GetVerticalValue()
    {
        return _inputVector.y != _nullValue ? _inputVector.y : Input.GetAxis(_axisVertical);
    }
}