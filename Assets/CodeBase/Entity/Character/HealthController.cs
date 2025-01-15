using CodeBase.Common.Interface;
using UniRx;

namespace CodeBase.Entity.Character
{
    public abstract class HealthController : ILogic
    {
        private readonly int _maxHealth;
        private readonly ReactiveProperty<int> _currentHealth;
        private readonly UI_HealthBar _healthBar;


        protected HealthController(int maxHealth, UI_HealthBar healthBar)
        {
            _maxHealth = maxHealth;
            _healthBar = healthBar;
            _currentHealth = new ReactiveProperty<int>(_maxHealth);
        }

        public void TakeDamage(int damage)
        {
            _currentHealth.Value -= damage;
            if (_currentHealth.Value <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
        }

        public void Enter()
        {
            _healthBar.Initialization(_currentHealth);
            _currentHealth.Value = _maxHealth;
        }

        public void Exit()
        {
            
        }
    }
}