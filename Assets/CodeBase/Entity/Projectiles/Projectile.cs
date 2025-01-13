using CodeBase.Entity.Character.Enemy;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Lean.Pool;
using UnityEngine;

namespace CodeBase.Entity.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        private const string ENEMY_TAG = "Enemy";
        
        [SerializeField] private float speed;
        [SerializeField] private float maxDistance;
        [SerializeField] private int damage;
        
        private Transform _target;
        private Vector3 _startPosition;
        
        private TweenerCore<Vector3, Vector3, VectorOptions> _tween;
        
        public void Init(Transform target)
        {
            _target = target;
            _startPosition = transform.position;
            Move();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if( other.CompareTag(ENEMY_TAG))
            {
                TakeDamage(other.transform.root.GetComponent<Enemy>());
            }
        }
        
        private void Move()
        {
            if(_target == null) return;
            
            float distance = Vector3.Distance(transform.position, _target.transform.position);

            if (distance > maxDistance)
            {
                distance = maxDistance; 
                _target.position = _startPosition + (_target.position - _startPosition).normalized * maxDistance;
            }
            
            
            float duration = distance / speed;
            
            _tween = transform.DOMove(_target.transform.position, duration).SetEase(Ease.Linear) .OnComplete(() => DestroyProjectile());
        }

        private void TakeDamage(Enemy enemy)
        {
            enemy.TakeDamage(damage);
            _target = null;
            _tween.Kill();
            DestroyProjectile();
        }

        private void DestroyProjectile()
        {
            _target = null;
            LeanPool.Despawn(this);
        }
    }
}
