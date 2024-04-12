using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class SliderPanel : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Slider _slider;
        
        [Header("Data")]
        [SerializeField] private float _minSliderValue = 0.2f;
        [SerializeField] private float _maxSliderValue = 1.0f;
        
        public void SetEnabled(bool value)
        {
            gameObject.SetActive(value);
        }
        
        public void SetFillAmount(float value)
        {
            var clampedValue = Mathf.Clamp(value, _minSliderValue, _maxSliderValue);

            _slider.value = clampedValue;
        }
    }
}