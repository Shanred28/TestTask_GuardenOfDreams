using CodeBase.Common.Interface;
using CodeBase.Entity.InventorySystem;
using CodeBase.Entity.Projectiles;
using Lean.Pool;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Entity.Character.Player
{
    public class PlayerAttack : ILogic
    {
        private readonly PlayerLogic _playerLogic;
        private readonly Projectile _projectile;
        private readonly Transform _firePoint;
        private readonly UnityEvent _onFire;
        private readonly PlayerInventory _inventory;

        public PlayerAttack(Projectile projectile, Transform firePoint, UnityEvent fire,PlayerLogic playerLogic,PlayerInventory inventory)
        {
            _playerLogic = playerLogic;
            _inventory = inventory;
            _projectile = projectile;
            _firePoint = firePoint;
            _onFire = fire;
            _onFire.AddListener(Shoot);
        }

        public void Enter()
        {
            
        }

        private void Shoot()
        {
            if (_inventory.UseAmmo(1))
            {
                LeanPool.Spawn(_projectile, _firePoint.position, Quaternion.identity).Init(_playerLogic.GetTarget());
            }
            else
            {
                Debug.Log("No ammo left!");
            }
        }

        public void Exit()
        {
            _onFire.RemoveListener(Shoot);
        }
    }
}
