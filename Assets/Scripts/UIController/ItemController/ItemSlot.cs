using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    Item item;
    [SerializeField] Image icon;
    [SerializeField] Button removeBtn;

    public void AddItem(Item newItem) {
        item = newItem;
        
        icon.sprite = item.icon;
        icon.enabled = true;
        removeBtn.interactable = true;
    }

    public void ClearSlot() {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeBtn.interactable = false;
    }

    public void OnRemoveBtn() {
        Inventory.invenInstance.Remove(item);
    }

    public void UseItem() {
        if (item != null) {
            item.Use();
        }
    }
}
