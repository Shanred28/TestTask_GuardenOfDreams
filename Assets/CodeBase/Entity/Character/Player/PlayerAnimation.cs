using System;
using CodeBase.Common.Interface;
using UniRx;
using UnityEngine;

namespace CodeBase.Entity.Character.Player
{
    public class PlayerAnimation : ILogic
    {
        private static readonly int IsMove = Animator.StringToHash("IsMove");
        private const float Interval = 0.5f;

        private readonly Animator _animator;
        private readonly SpriteRenderer[] _spritesRenderer;
        private readonly MovementController _movement;
        private readonly Transform _playerTransform;

        private bool _isEnemyNearest;
        private IDisposable _disposableInterval;
        
        public PlayerAnimation(Animator animator, MovementController movement, SpriteRenderer[] spritesRenderer,
            Transform playerTransform)
        {
            _animator = animator;
            _movement = movement;
            _spritesRenderer = spritesRenderer;
            _playerTransform = playerTransform;
        }

        public void Enter()
        {
            _movement.OnLookRight += MoveSpriteFlipX;
            _movement.OnMove += OnMoveAnimation;
        }

        public void FlipOnEnemy(Transform enemy)
        {
            _isEnemyNearest = enemy;
            _disposableInterval?.Dispose();
            
            if(!enemy) return;
            
            _disposableInterval = Observable.Interval(TimeSpan.FromSeconds(Interval)).Subscribe(_ =>
            {
                foreach (var t in _spritesRenderer)
                {
                    t.flipX = enemy.position.x < _playerTransform.position.x;
                }
            });
        }

        private void OnMoveAnimation(bool obj) => _animator.SetBool(IsMove, obj);

        private void MoveSpriteFlipX(bool isRight)
        {
            if (_isEnemyNearest) return;

            foreach (var t in _spritesRenderer)
            {
                t.flipX = !isRight;
            }
        }

        public void Exit()
        {
            _movement.OnLookRight -= MoveSpriteFlipX;
            _disposableInterval?.Dispose();
        }
    }
}