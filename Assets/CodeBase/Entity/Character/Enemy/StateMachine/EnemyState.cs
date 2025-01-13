namespace CodeBase.Entity.Character.Enemy.StateMachine
{
    public abstract class EnemyState
    {
        protected readonly Enemy Enemy;

        protected EnemyState(Enemy enemy)
        {
            Enemy = enemy;
        } 
        
        public abstract void Enter(); 
        public abstract void Execute(); 
        public abstract void Exit();
    }
}
