using System;
using CodeBase.Common;
using CodeBase.Entity.InventorySystem;
using CodeBase.Entity.InventorySystem.Items;
using CodeBase.Entity.Projectiles;
using Lean.Pool;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Entity.Character.Player
{
    public class PlayerLogic : MonoBehaviour
    {
        public Action<Transform> OnEnemyNears;
        
        private static readonly int IsDie = Animator.StringToHash("IsDie");
        
        [SerializeField] private SpriteRenderer[] spriteRenderer;
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private Transform pointFire;
        [SerializeField] private Animator animator;
        [SerializeField] private float timeDie = 1f;
        [SerializeField] private GameObject weapon;

        private Joystick _joystick;
        private MovementController _movementController;
        private PlayerHealthController _playerHealthController;
        private PlayerInventory _playerInventory;
        private PlayerAttack _playerAttack;
        private PlayerAnimation _playerAnimation;
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag(TagLayerNameHolder.TAG_ENEMY))
            {
                OnEnemyNears?.Invoke(other.transform);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(TagLayerNameHolder.TAG_ENEMY))
            {
                OnEnemyNears?.Invoke(other.transform);
            }
        }

        private void OnDestroy()
        {
            _movementController.Exit();
            _playerHealthController.Exit();
            _playerAttack.Exit();
            _playerAnimation.Exit();
        }

        public void Initialization(Joystick joystick, int maxHP, float moveSpeed,UI_HealthBar healthBar,PlayerInventory inventory,Button fireButton)
        {
            _joystick = joystick;
            _movementController = new MovementController(this,moveSpeed,_joystick);
            _playerHealthController = new PlayerHealthController(maxHP,healthBar);
            _playerHealthController.OnDead +=  Die;
            _playerInventory = inventory;
            _playerAnimation = new PlayerAnimation(animator,_movementController,spriteRenderer, transform);
            _playerAttack = new PlayerAttack(projectilePrefab,pointFire,fireButton, this,_playerInventory, _playerAnimation);
            
            
            _playerHealthController.Enter();
            _movementController.Enter();
            _playerAttack.Enter();
            _playerAnimation.Enter();
        }

        public void TakeDamage(int damage) => _playerHealthController.TakeDamage(damage);

        public void PickupItem(ItemSO item)
        {
            _playerInventory.AddItem(item);
        }

        private void Die()
        {
            _playerHealthController.OnDead -=  Die;
            weapon.SetActive(false);
            animator.SetBool(IsDie,true);

            Observable.Timer(TimeSpan.FromSeconds(timeDie)).Subscribe(_ =>
            {
                LeanPool.Despawn(gameObject);
            });
        }
    }
}
