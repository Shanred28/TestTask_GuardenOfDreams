using UnityEngine;

namespace CodeBase.Entity.InventorySystem.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class ItemSO : ScriptableObject
    {
        public Item itemPrefab;
        public string itemName;
        public Sprite icon;
        public int amountItem;
        public bool isStackable;
        public int maxStackSize;
        public bool isAmmo;
    }
}
