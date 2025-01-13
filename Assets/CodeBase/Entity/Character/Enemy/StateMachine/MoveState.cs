using UnityEngine;

namespace CodeBase.Entity.Character.Enemy.StateMachine
{
    public class MoveState : EnemyState
    {
        public MoveState(Enemy enemy) : base(enemy)
        {
        }

        public override void Enter()
        {

        }

        public override void Execute()
        {
            Enemy.MoveTowardsPlayer();

            if (Enemy.IsPlayerInAttackRange())
            {
                Enemy.ChangeState(new AttackState(Enemy));
            }
        }

        public override void Exit()
        {

        }
    }
}
