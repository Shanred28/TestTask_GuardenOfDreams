using UnityEngine;

namespace CodeBase.Entity.Character.Enemy.StateMachine
{
    public class AttackState : EnemyState
    {
        public AttackState(Enemy enemy) : base(enemy)
        {
        }

        public override void Enter()
        {
            
        }

        public override void Execute()
        {
            Enemy.AttackPlayer();

            if (!Enemy.IsPlayerInAttackRange())
            {
                Enemy.ChangeState(new MoveState(Enemy));
            }
        }

        public override void Exit()
        {
            
        }
    }
}
