using UnityEngine;

namespace CodeBase.Entity.Character.Enemy
{
    public class EnemyAnimation : CharacterAnimation
    {
        private readonly Enemy _enemy;
        public EnemyAnimation(SpriteRenderer[] spritesRenderer, Transform targetTransform,Enemy enemy) : base(spritesRenderer, targetTransform)
        {
            _enemy = enemy;
            _enemy.OnLookRight += MoveSpriteFlipX;
        }

        public override void Exit()
        {
            _enemy.OnLookRight += MoveSpriteFlipX;
            base.Exit();
        }
    }
}
