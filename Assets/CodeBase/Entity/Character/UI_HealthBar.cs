using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Entity.Character
{
    public class UI_HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Image fill;
        
        public void Initialization(ReactiveProperty<int> healthReactiveProperty) 
        { 
            slider.maxValue = healthReactiveProperty.Value; 
            slider.value = healthReactiveProperty.Value;
            
            healthReactiveProperty.
                Subscribe(currentHealth =>
                {
                    slider.value = currentHealth; 
                }) .AddTo(this);
        }
    }
}
