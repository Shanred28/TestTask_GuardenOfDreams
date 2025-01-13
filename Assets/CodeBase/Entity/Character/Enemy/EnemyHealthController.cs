using CodeBase.Entity.InventorySystem.Items;
using Lean.Pool;

namespace CodeBase.Entity.Character.Enemy
{
    public class EnemyHealthController : HealthController
    {
        private readonly Enemy _enemy;
        public EnemyHealthController(int maxHealth, UI_HealthBar healthBar,Enemy enemy) : base(maxHealth, healthBar)
        {
            _enemy = enemy;
        }

        protected override void Die()
        {
            
            _enemy.Die();
            base.Die();
        }
    }
}
