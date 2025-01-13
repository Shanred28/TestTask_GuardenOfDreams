using System.Collections.Generic;
using CodeBase.Entity.InventorySystem.Items;
using UnityEngine;

namespace CodeBase.Entity.InventorySystem
{
    public class PlayerInventory : MonoBehaviour
    {

         [SerializeField] private List<InventorySlot> inventorySlots;
         [SerializeField] private GameObject panel;
         
        public void OnClickButton()
        {
            panel.SetActive(!panel.activeSelf);
        }

        public void AddItem(ItemSO itemSo)
        {
            int remainingQuantity = itemSo.amountItem;
            
            foreach (var slot in inventorySlots)
            {
                if (slot.ItemSo == itemSo && slot.ItemSo.isStackable && slot.Quantity < slot.ItemSo.maxStackSize)
                {
                    int availableSpace = slot.ItemSo.maxStackSize - slot.Quantity;
                    int quantityToAdd = Mathf.Min(remainingQuantity, availableSpace);
                    slot.SetQuantity(slot.Quantity + quantityToAdd);
                    remainingQuantity -= quantityToAdd;
                    
                    if (remainingQuantity <= 0)
                    {
                        return;
                    }
                }
            }

            foreach (var slot in inventorySlots)
            {
                if (slot.ItemSo == null)
                {
                    slot.AddItem(itemSo, remainingQuantity); 
                    return;
                }
            } 
            
            Debug.Log("Inventory is full");
        }
        
        public bool UseAmmo(int amount)
        {
            int remainingAmmo = amount;
            
            foreach (var slot in inventorySlots)
            {
                if (slot.ItemSo == null || !slot.ItemSo.isAmmo) continue;
                
                if (slot.Quantity < remainingAmmo) continue;
                    
                slot.SetQuantity(slot.Quantity - remainingAmmo);
                        
                if (slot.Quantity != 0) return true;
                        
                slot.SetQuantity(0);
                slot.RemoveItem();

                return true;
            }

            Debug.Log("Not enough ammo!");
            return false;
        }

        public void DeleteSelectedItem(InventorySlot slot)
        {
            foreach (var s in inventorySlots)
            {
                if (s != slot) continue;
                
                slot.RemoveItem(); 
                return;
            }
        }
    }
}
