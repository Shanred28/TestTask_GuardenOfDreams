
namespace CodeBase.Entity.Character.Player
{
    public class PlayerHealthController : HealthController
    {


        public PlayerHealthController(int maxHealth, UI_HealthBar healthBar) : base(maxHealth,healthBar)
        {
        
        }

        protected override void Die()
        {
            base.Die();
        }
    }
}
