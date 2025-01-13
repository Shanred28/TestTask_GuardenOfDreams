using CodeBase.Entity.Character;
using CodeBase.Entity.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Player
{
    public class UI_PlayerHUDHolder : MonoBehaviour
    {
        public Joystick Joystick => joystick;
        public PlayerInventory PlayerInventory => playerInventory;
        public Button FireButton=> fireButton;
        public UI_HealthBar HealthBar=> healthBar;
        
        [SerializeField] private Joystick joystick;
        [SerializeField] private PlayerInventory playerInventory;
        [SerializeField] private Button fireButton;
        [SerializeField] private UI_HealthBar healthBar;
    }
}
