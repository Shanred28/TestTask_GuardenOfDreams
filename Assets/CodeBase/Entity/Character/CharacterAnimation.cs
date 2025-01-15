using System;
using CodeBase.Common.Interface;
using UniRx;
using UnityEngine;

namespace CodeBase.Entity.Character
{
    public abstract class CharacterAnimation : ILogic
    {
        private const float Interval = 0.5f;
        
        private readonly SpriteRenderer[] _spritesRenderer;
        private readonly Transform _targetTransform;

        private bool _isEnemyNearest;
        private IDisposable _disposableInterval;


        protected CharacterAnimation(SpriteRenderer[] spritesRenderer, Transform targetTransform)
        {
            _spritesRenderer = spritesRenderer;
            _targetTransform = targetTransform;
        }

        public virtual void Enter()
        {
        }
        
        public  void FlipOnEnemy(Transform enemy)
        {
            _isEnemyNearest = enemy;
            _disposableInterval?.Dispose();
            
            if(!enemy) return;
            
            _disposableInterval = Observable.Interval(TimeSpan.FromSeconds(Interval)).Subscribe(_ =>
            {
                foreach (var t in _spritesRenderer)
                {
                    t.flipX = enemy.position.x < _targetTransform.position.x;
                }
            });
        }
        
        protected void MoveSpriteFlipX(bool isRight)
        {
            if (_isEnemyNearest) return;

            foreach (var t in _spritesRenderer)
            {
                t.flipX = !isRight;
            }
        }
        

        public virtual void Exit()
        {
            _disposableInterval?.Dispose();
        }
    }
}
