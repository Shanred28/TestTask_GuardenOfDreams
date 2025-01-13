using System;
using CodeBase.Common.Interface;
using UniRx;
using UnityEngine;

namespace CodeBase.Entity.Character.Player
{
    public class MovementController : ILogic
    {
        private readonly PlayerLogic _playerLogic;

        private readonly Joystick _joystick;

        private readonly float _moveSpeed;
        private Vector2 _movement;
        private readonly Rigidbody2D _rigidbody;
        private readonly SpriteRenderer[] _spriteRenderer;

        private IDisposable _subscriptionUpdate;
        private IDisposable _subscriptionFixedUpdate;

        public MovementController(PlayerLogic playerLogic, float moveSpeed, Joystick joystick, SpriteRenderer[] spriteRenderer)
        {
            _playerLogic = playerLogic;
            _rigidbody = playerLogic.GetComponent<Rigidbody2D>();
            _moveSpeed = moveSpeed;
            _joystick = joystick;
            _spriteRenderer = spriteRenderer;
        }

        public void Enter()
        {
           _subscriptionUpdate = Observable.EveryUpdate().Subscribe(_ => UpdateInputEveryFrame()).AddTo(_playerLogic);
           _subscriptionFixedUpdate =Observable.EveryFixedUpdate().Subscribe(_ => FixedUpdateMove()).AddTo(_playerLogic);
        }

        private void UpdateInputEveryFrame()
        {
            _movement.x = _joystick.Horizontal;
            _movement.y = _joystick.Vertical;
            
            SpriteFlipX();
        }

        private void FixedUpdateMove()
        {
            _rigidbody.MovePosition(_rigidbody.position + _movement *  _moveSpeed * Time.fixedDeltaTime );
        }

        private void SpriteFlipX()
        {
            switch (_movement.x)
            {
                case < 0:
                {
                    foreach (var t in _spriteRenderer)
                    {
                        t.flipX = true;
                    }

                    break;
                }
                case > 0:
                {
                    foreach (var t in _spriteRenderer)
                    {
                        t.flipX = false;
                    }

                    break;
                }
            }
        }

        public void Exit()
        {
            _subscriptionUpdate.Dispose();
            _subscriptionFixedUpdate.Dispose();
        }
    }
}