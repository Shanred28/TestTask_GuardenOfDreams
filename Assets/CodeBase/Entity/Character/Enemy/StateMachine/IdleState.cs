using UnityEngine;

namespace CodeBase.Entity.Character.Enemy.StateMachine
{
    public class IdleState : EnemyState
    {
        public IdleState(Enemy enemy) : base(enemy)
        {
        }

        public override void Enter()
        {

        }

        public override void Execute()
        {
            if (Enemy.IsPlayerInSight())
            {
                Enemy.ChangeState(new MoveState(Enemy));
            }
        }

        public override void Exit()
        {

        }
    }
}
