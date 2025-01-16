using System;

namespace CodeBase.Entity.Character.Player
{
    public class PlayerHealthController : HealthController
    {
        public Action OnDead;

        public PlayerHealthController(int maxHealth, UI_HealthBar healthBar) : base(maxHealth,healthBar)
        {
        
        }

        protected override void Die()
        {
            OnDead?.Invoke();
            base.Die();
        }
    }
}
