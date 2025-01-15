using System.Collections.Generic;
using CodeBase.Entity.InventorySystem;
using CodeBase.Entity.InventorySystem.Items;
using CodeBase.Entity.Projectiles;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Entity.Character.Player
{
    public class PlayerLogic : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] spriteRenderer;
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private Transform pointFire;
        [SerializeField] private Animator animator;

        private Joystick _joystick;
        private MovementController _movementController;
        private PlayerHealthController _playerHealthController;
        private PlayerInventory _playerInventory;
        private PlayerAttack _playerAttack;
        private PlayerAnimation _playerAnimation;
        private Button _fireButton;

        public Transform GetTarget() => _targetEnemy;
        private readonly List<Transform> _enemiesInRange = new List<Transform>();
        private Transform _targetEnemy;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Enemy"))
            {
                _enemiesInRange.Add(other.transform);
                UpdateTargetEnemy();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                _enemiesInRange.Remove(other.transform);
                UpdateTargetEnemy();
            }
        }

        private void OnDestroy()
        {
            _movementController.Exit();
            _playerHealthController.Exit();
            _playerAttack.Exit();
        }

        public void Initialization(Joystick joystick, int maxHP, float moveSpeed,UI_HealthBar healthBar,PlayerInventory inventory,Button fireButton)
        {
            _joystick = joystick;
            _fireButton = fireButton;
            _movementController = new MovementController(this,moveSpeed,_joystick);
            _playerHealthController = new PlayerHealthController(maxHP,healthBar);
            _playerInventory = inventory;
            _playerAttack = new PlayerAttack(projectilePrefab,pointFire,_fireButton.onClick, this,_playerInventory);
            _playerAnimation = new PlayerAnimation(animator,_movementController,spriteRenderer, transform);
            
            _playerHealthController.Enter();
            _movementController.Enter();
            _playerAttack.Enter();
            _playerAnimation.Enter();

            FireButtonInteractable();
        }

        public void TakeDamage(int damage) => _playerHealthController.TakeDamage(damage);

        public void PickupItem(ItemSO item)
        {
            _playerInventory.AddItem(item);
        }

        private void UpdateTargetEnemy() 
        {
            if (_enemiesInRange.Count == 0)
            {
                _targetEnemy = null; 
                FireButtonInteractable();
                _playerAnimation.FlipOnEnemy(_targetEnemy);
                return;
            } 
            
            _targetEnemy = GetClosestEnemy();
            _playerAnimation.FlipOnEnemy(_targetEnemy);
            FireButtonInteractable();
        }

        private Transform GetClosestEnemy()
        {
            Transform closestEnemy = null;
            float closestDistance = Mathf.Infinity;
            foreach (Transform enemy in _enemiesInRange)
            {
                float distance = Vector3.Distance(transform.position, enemy.position);
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
    }
}
