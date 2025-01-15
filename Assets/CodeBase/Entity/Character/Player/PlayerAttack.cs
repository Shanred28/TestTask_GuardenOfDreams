using System.Collections.Generic;
using CodeBase.Common.Interface;
using CodeBase.Entity.InventorySystem;
using CodeBase.Entity.Projectiles;
using Lean.Pool;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Entity.Character.Player
{
    public class PlayerAttack : ILogic
    {
        private readonly PlayerInventory _inventory;
        private readonly PlayerLogic _playerLogic;
        private readonly PlayerAnimation _animation;
        
        private readonly Projectile _projectile;
        private readonly Transform _firePoint;
        private readonly Button _fireButton;
        
        private readonly List<Transform> _enemiesInRange = new List<Transform>();
        private Transform _targetEnemy;

        public PlayerAttack(Projectile projectile, Transform firePoint, Button fireButton,PlayerLogic playerLogic,PlayerInventory inventory,PlayerAnimation animation)
        {
            _playerLogic = playerLogic;
            _inventory = inventory;
            _projectile = projectile;
            _firePoint = firePoint;
            _animation = animation;
            _fireButton = fireButton;
            _fireButton.onClick.AddListener(Shoot);
        }

        public void Enter()
        {
            _playerLogic.OnEnemyNears += UpdateListEnemy;
        }

        private void Shoot()
        {
            if (_inventory.UseAmmo(1))
            {
                LeanPool.Spawn(_projectile, _firePoint.position, Quaternion.identity).Init(_targetEnemy);
            }
            else
            {
                Debug.Log("No ammo left!");
            }
        }

        private void UpdateListEnemy(Transform enemy)
        {
            if (_enemiesInRange.Contains(enemy))
            {
                _enemiesInRange.Remove(enemy);
            }
            else
            {
                _enemiesInRange.Add(enemy);
            }

            UpdateTargetEnemy();
        }

        private void UpdateTargetEnemy() 
        {
            if (_enemiesInRange.Count == 0)
            {
                _targetEnemy = null; 
                FireButtonInteractable();
                _animation.FlipOnEnemy(_targetEnemy);
                return;
            } 
            
            _targetEnemy = GetClosestEnemy();
            _animation.FlipOnEnemy(_targetEnemy);
            FireButtonInteractable();
        }

        private Transform GetClosestEnemy()
        {
            Transform closestEnemy = null;
            float closestDistance = Mathf.Infinity;
            foreach (Transform enemy in _enemiesInRange)
            {
                float distance = Vector3.Distance(_playerLogic.transform.position, enemy.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
            
            return closestEnemy;
        }
        
        private void FireButtonInteractable()
        {
            _fireButton.interactable = _targetEnemy != null;
        }

        public void Exit()
        {
            _playerLogic.OnEnemyNears -= UpdateListEnemy;
            _fireButton.onClick.RemoveListener(Shoot);
        }
    }
}
