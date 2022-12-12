using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Transform itemsContainer;
    Inventory inventory;
    ItemSlot[] slots;

    void Start() {
        inventory = Inventory.invenInstance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsContainer.GetComponentsInChildren<ItemSlot>();
    }

    void UpdateUI() {
        // Debug.Log("Updating UI!");
        for (int i = 0; i < slots.Length; i++) {
            if (i < inventory.itemList.Count) {
                slots[i].AddItem(inventory.itemList[i]);
            } else {
                slots[i].ClearSlot();
            }
        }
    }
}
