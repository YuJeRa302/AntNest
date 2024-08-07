using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Game.Scripts
{
    public class DotView : MonoBehaviour
    {
        private const float _minValueVector2 = 0f;
        private const float _minMiddleAverageValue = 0.25f;
        private const float _middleMaxAverageValue = 0.75f;
        private const float _defaultHorizontalValue = 0f;
        private const float _defaultVerticalValue = 1f;

        private readonly Color _defaultColor = Color.white;
        private readonly Color _selectedColor = Color.green;

        [SerializeField] private Slider _slider;
        [SerializeField] private Image _firstDot;
        [SerializeField] private Image _middleDot;
        [SerializeField] private Image _lastDot;

        private ScrollRect _scroll;

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(OnChangeSliderValue);

            if (_scroll != null)
                _scroll.onValueChanged.RemoveListener(OnChangeVectorValue);
        }

        public void SetScrollRect(ScrollRect scrollRect)
        {
            _scroll = scrollRect;
            _scroll.onValueChanged.AddListener(OnChangeVectorValue);
            _slider.onValueChanged.AddListener(OnChangeSliderValue);
            LoadDefauitParameters();
        }

        private void LoadDefauitParameters()
        {
            _scroll.horizontalNormalizedPosition = _defaultHorizontalValue;
            _scroll.verticalNormalizedPosition = _defaultVerticalValue;

            if (_scroll.horizontal)
                _slider.value = _scroll.horizontalNormalizedPosition;
            else
                _slider.value = _scroll.verticalNormalizedPosition;

            SetDotsDefaultColor();
            _firstDot.color = _selectedColor;
        }

        private void OnChangeVectorValue(Vector2 vector2)
        {
            SetDotsDefaultColor();

            if (_scroll.horizontal)
                ChangeHorizontalVectorValue(vector2);
            else
                ChangeVerticalVectorValue(vector2);
        }

        private void OnChangeSliderValue(float value)
        {
            if (_scroll.horizontal)
                _scroll.horizontalNormalizedPosition = value;
            else
                _scroll.verticalNormalizedPosition = value;
        }

        private void ChangeVerticalVectorValue(Vector2 vector2)
        {
            _slider.value = vector2.y;

            if (vector2.normalized.y >= _minValueVector2 && vector2.y < _minMiddleAverageValue)
                _lastDot.color = _selectedColor;
            else if (vector2.y >= _minMiddleAverageValue && vector2.y < _middleMaxAverageValue)
                _middleDot.color = _selectedColor;
            else
                _firstDot.color = _selectedColor;
        }

        private void ChangeHorizontalVectorValue(Vector2 vector2)
        {
            _slider.value = vector2.x;

            if (vector2.normalized.x >= _minValueVector2 && vector2.x < _minMiddleAverageValue)
                _firstDot.color = _selectedColor;
            else if (vector2.x >= _minMiddleAverageValue && vector2.x < _middleMaxAverageValue)
                _middleDot.color = _selectedColor;
            else
                _lastDot.color = _selectedColor;
        }

        private void SetDotsDefaultColor()
        {
            _firstDot.color = _defaultColor;
            _middleDot.color = _defaultColor;
            _lastDot.color = _defaultColor;
        }
    }
}