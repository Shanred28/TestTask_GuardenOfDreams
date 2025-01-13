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
        [SerializeField] private int maxHP;
        [SerializeField] private float distanceSight;
        [SerializeField] private float attackRange;
        [SerializeField] private float moveSpeed;
        [SerializeField] private UI_HealthBar healthBar;
        [SerializeField] private Animator animator;
        [SerializeField] private int damage;
        [SerializeField] private float intervalAttack;

        [SerializeField] private ItemSO itemSo;
        
        private Transform _player;
        private EnemyHealthController _enemyHealthController;
        private EnemyState _currentState;
        private bool _isAttacking;
        private IDisposable _disposable;
        private IDisposable _disposableIntervalAttack;

        public void Initialization(Transform player)
        {
            _player = player;
            
            _enemyHealthController = new EnemyHealthController(maxHP,healthBar,this);
            
            _enemyHealthController.Enter();
            
            ChangeState(new IdleState(this));
            
            _disposable =Observable.EveryUpdate()
                .Subscribe(_ => CheckState()).AddTo(this);
        }
        
        private void CheckState()
        {
            _currentState.Execute();
        }
        
        public void TakeDamage(int damage) => _enemyHealthController.TakeDamage(damage);
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
            Vector3 direction = (_player.position - transform.position).normalized; 
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        public void AttackPlayer()
        {
            if(_isAttacking) return;
            
            _isAttacking = true;
            _disposableIntervalAttack = Observable.Interval(TimeSpan.FromSeconds(intervalAttack) )
                .Subscribe(_ => 
                {
                    animator.SetTrigger("Hit");
                    _player.GetComponent<PlayerLogic>().TakeDamage(damage);
                }).AddTo(this);
        }

        public void Die()
        {
            _currentState.Exit();
            _disposable.Dispose();
            _disposableIntervalAttack?.Dispose();
            LeanPool.Spawn(itemSo.itemPrefab, transform.position, Quaternion.identity);
            LeanPool.Despawn(gameObject);
        }
    }
}
