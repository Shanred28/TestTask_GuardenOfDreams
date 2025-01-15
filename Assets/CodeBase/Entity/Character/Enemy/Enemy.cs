using System;
using CodeBase.Entity.Character.Enemy.StateMachine;
using CodeBase.Entity.Character.Player;
using CodeBase.Entity.InventorySystem.Items;
using Lean.Pool;
using UniRx;
using UnityEngine;

namespace CodeBase.Entity.Character.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public Action<bool> OnLookRight;
        
        private static readonly int Hit = Animator.StringToHash("Hit");
        
        [SerializeField] private int maxHP;
        [SerializeField] private float distanceSight;
        [SerializeField] private float attackRange;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float stopDistance = 1.5f;
        [SerializeField] private UI_HealthBar healthBar;
        [SerializeField] private Animator animator;
        [SerializeField] private int damage;
        [SerializeField] private float intervalAttack;
        [SerializeField] private SpriteRenderer[] spritesRenderer;

        [SerializeField] private ItemSO itemSo;
        
        private Transform _player;
        private EnemyHealthController _enemyHealthController;
        private EnemyState _currentState;
        private Rigidbody2D _rigidbody;
        private bool _isAttacking;
        private IDisposable _disposable;
        private IDisposable _disposableIntervalAttack;
        private bool _isRightMoving = true;
        
        private EnemyAnimation _enemyAnimation;

        public void Initialization(Transform player)
        {
            _player = player;
            _rigidbody = GetComponent<Rigidbody2D>();
            _enemyHealthController = new EnemyHealthController(maxHP,healthBar,this);
            
            _enemyHealthController.Enter();
            
            ChangeState(new IdleState(this));
            
            _disposable =Observable.EveryUpdate()
                .Subscribe(_ => CheckState()).AddTo(this);

            _enemyAnimation = new EnemyAnimation(spritesRenderer,transform,this);
            _enemyAnimation.Enter();
        }
        
        private void CheckState()
        {
            _currentState.Execute();
        }
        
        public void TakeDamage(int takeDamage) => _enemyHealthController.TakeDamage(takeDamage);
        public void ChangeState(EnemyState newState)
        {
            _currentState?.Exit();
            
            _disposableIntervalAttack?.Dispose();
            _isAttacking = false;
            _currentState = newState;
            _currentState.Enter();
        }

        public bool IsPlayerInSight() => Vector3.Distance(_player.position, transform.position) < distanceSight;

        public bool IsPlayerInAttackRange() => Vector3.Distance(transform.position, _player.position) < attackRange;

        public void MoveTowardsPlayer()
        {
            float distance = Vector3.Distance(transform.position, _player.position);
            if (distance > stopDistance)
            {
                Vector3 direction = (_player.position - transform.position).normalized;
                CheckFlipSprite(direction.x);
                _rigidbody.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
            }
        }

        public void AttackPlayer()
        {
            if(_isAttacking) return;
            
            _isAttacking = true;
            _disposableIntervalAttack = Observable.Interval(TimeSpan.FromSeconds(intervalAttack) )
                .Subscribe(_ => 
                {
                    animator.SetTrigger(Hit);
                    _player.GetComponent<PlayerLogic>().TakeDamage(damage);
                }).AddTo(this);
        }
        
        private void CheckFlipSprite(float moveX)
        {
            if ((!_isRightMoving || !(moveX < 0)) && (_isRightMoving || !(moveX > 0))) return;
            
            _isRightMoving = moveX > 0; 
            OnLookRight?.Invoke(_isRightMoving);
        }
        

        public void Die()
        {
            _enemyAnimation.Exit();
            _currentState.Exit();
            _disposable.Dispose();
            _disposableIntervalAttack?.Dispose();
            LeanPool.Spawn(itemSo.itemPrefab, transform.position, Quaternion.identity);
            LeanPool.Despawn(gameObject);
        }
    }
}
