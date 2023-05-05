using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image _joystickBackgorund;
    private Image _joystick;
    private Vector2 _inputVector;

    private void Start()
    {
        _joystickBackgorund = GetComponent<Image>();
        _joystick = transform.GetChild(0).GetComponent<Image>();
    }

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
            _inputVector = new Vector2(position.x * 2, position.y * 2);
            _inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;
            _joystick.rectTransform.anchoredPosition = new Vector2(_inputVector.x * (_joystickBackgorund.rectTransform.sizeDelta.x / 2), _inputVector.y * (_joystickBackgorund.rectTransform.sizeDelta.y / 2));
        }
    }

    public float GetHorizontalValue()
    {
        return _inputVector.x != 0 ? _inputVector.x : Input.GetAxis("Horizontal");
    }

    public float GetVerticalValue()
    {
        return _inputVector.y != 0 ? _inputVector.y : Input.GetAxis("Vertical");
    }
}