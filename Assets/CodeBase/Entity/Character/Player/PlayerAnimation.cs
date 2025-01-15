using UnityEngine;

namespace CodeBase.Entity.Character.Player
{
    public class PlayerAnimation : CharacterAnimation
    {
        private static readonly int IsMove = Animator.StringToHash("IsMove");

        private readonly Animator _animator;
        private readonly MovementController _movement;
        
        public PlayerAnimation(Animator animator, MovementController movement, SpriteRenderer[] spritesRenderer,
            Transform playerTransform) : base(spritesRenderer, playerTransform)
        {
            _animator = animator;
            _movement = movement;
        }

        public override void Enter()
        {
            _movement.OnLookRight += MoveSpriteFlipX;
            _movement.OnMove += OnMoveAnimation;
        }
        
        private void OnMoveAnimation(bool obj) => _animator.SetBool(IsMove, obj);
        
        public override void Exit()
        {
            _movement.OnLookRight -= MoveSpriteFlipX;
            base.Exit();
        }
    }
}