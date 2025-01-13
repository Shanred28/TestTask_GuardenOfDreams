using CodeBase.Entity.InventorySystem.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Entity.InventorySystem
{
    public class InventorySlot : MonoBehaviour
    {
        public ItemSO ItemSo => _itemSo;
        public int Quantity => _quantity;
        
        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI itemAmount;
        [SerializeField] private Button removeButton;
        [SerializeField] private PlayerInventory inventory;
        [SerializeField] private Sprite defaultItemIcon;
        
        private ItemSO _itemSo; 
        private int _quantity;
        
        private void Start()
        {
            removeButton.onClick.AddListener(OnSlotClicked);

            if (!_itemSo)
            {
                removeButton.gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            removeButton.onClick.RemoveListener(OnSlotClicked);
        }

        public void AddItem(ItemSO newItemSo, int quantity)
        {
            _itemSo = newItemSo; 
            itemIcon.sprite = _itemSo.icon;
            _quantity = quantity;
            removeButton.gameObject.SetActive(true);

            if (_itemSo.isStackable)
            {
                itemAmount.text = _quantity.ToString();
            }
            else
                itemAmount.text = "";
        }

        public void SetQuantity(int quantity)
        {
            _quantity = quantity;
            itemAmount.text = _quantity.ToString();
        }
        
        public void RemoveItem()
        {
            _itemSo = null; 
            _quantity = 0;
            
            itemIcon.sprite = defaultItemIcon;
            removeButton.gameObject.SetActive(false);
            itemAmount.text = "";
        }

        private void OnSlotClicked()
        {
            if (ItemSo != null)
            {
                inventory.DeleteSelectedItem(this);
            }
        }
    }
}