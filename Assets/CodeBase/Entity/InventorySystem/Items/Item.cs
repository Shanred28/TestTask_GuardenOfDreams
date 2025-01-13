using CodeBase.Entity.Character.Player;
using Lean.Pool;
using UnityEngine;

namespace CodeBase.Entity.InventorySystem.Items
{
    public class Item : MonoBehaviour
    {
        private const string PLAYER_TAG = "Player";
        
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ItemSO itemSO;

        private void Start()
        {
            spriteRenderer.sprite = itemSO.icon;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if( other.CompareTag(PLAYER_TAG))
            {
                other.transform.root.GetComponent<PlayerLogic>().PickupItem(itemSO);
                PickupItem();
            }
        }

        private void PickupItem()
        {
            LeanPool.Despawn(this);
        }
    }
}
