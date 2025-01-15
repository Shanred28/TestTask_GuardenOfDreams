using System;
using CodeBase.Common.Interface;
using UniRx;
using UnityEngine;

namespace CodeBase.Entity.Character.Player
{
    public class MovementController : ILogic
    {
        public float MovementX => _movement.x;
        public Action<bool> OnLookRight;
        public Action<bool> OnMove;

        private readonly PlayerLogic _playerLogic;
        private readonly Joystick _joystick;

        private readonly float _moveSpeed;
        private Vector2 _movement;
        private readonly Rigidbody2D _rigidbody;
        private bool _isRightMoving = true;
        private bool _isMoving;

        private IDisposable _subscriptionUpdate;
        private IDisposable _subscriptionFixedUpdate;

        public MovementController(PlayerLogic playerLogic, float moveSpeed, Joystick joystick)
        {
            _playerLogic = playerLogic;
            _rigidbody = playerLogic.GetComponent<Rigidbody2D>();
            _moveSpeed = moveSpeed;
            _joystick = joystick;
        }

        public void Enter()
        {
            _subscriptionUpdate = Observable.EveryUpdate().Subscribe(_ => UpdateInputEveryFrame()).AddTo(_playerLogic);
            _subscriptionFixedUpdate =
                Observable.EveryFixedUpdate().Subscribe(_ => FixedUpdateMove()).AddTo(_playerLogic);
        }

        private void UpdateInputEveryFrame()
        {
            _movement.x = _joystick.Horizontal;
            _movement.y = _joystick.Vertical;

            CheckMovementStatus();
            CheckFlipSprite();
        }

        private void CheckMovementStatus()
        {
            bool isCurrentlyMoving = _movement.x != 0 || _movement.y != 0;
            
            if (isCurrentlyMoving && !_isMoving)
            {
                OnMove?.Invoke(true); _isMoving = true;
            } 
            else if (!isCurrentlyMoving && _isMoving)
            {
                OnMove?.Invoke(false); _isMoving = false;
            }
        }
        
        private void CheckFlipSprite()
        {
            if ((!_isRightMoving || !(_movement.x < 0)) && (_isRightMoving || !(_movement.x > 0))) return;

            _isRightMoving = _movement.x > 0;
            OnLookRight.Invoke(_isRightMoving);
        }

        private void FixedUpdateMove()
        {
            _rigidbody.MovePosition(_rigidbody.position + _movement * _moveSpeed * Time.fixedDeltaTime);
        }
        
        public void Exit()
        {
            _subscriptionUpdate.Dispose();
            _subscriptionFixedUpdate.Dispose();
        }
    }
}