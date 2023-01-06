using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Transform itemsContainer;
    [SerializeField] Transform equipContainer;
    Inventory inventory;
    ItemSlot[] slots;
    ItemSlot[] equipmentSlots;

    public static InventoryUI ivUIinstance;

    private void Awake() {
        if (ivUIinstance == null) {
            ivUIinstance = this;
        }
    }

    void Start() {
        inventory = Inventory.invenInstance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsContainer.GetComponentsInChildren<ItemSlot>();
        equipmentSlots = equipContainer.GetComponentsInChildren<ItemSlot>();
    }

    public void UpdateUI() {
        // Debug.Log(inventory.itemList[0]);
        for (int i = 0; i < slots.Length; i++) {
            if (i < inventory.itemList.Count) {
                slots[i].AddItem(inventory.itemList[i]);
            } else {
                slots[i].ClearSlot();
            }
        }
        if (EquipmentManager.instance.equipmentList[0] != null)
            equipmentSlots[0].AddItem(EquipmentManager.instance.equipmentList[0]);
        if (EquipmentManager.instance.equipmentList[1] != null)
            equipmentSlots[1].AddItem(EquipmentManager.instance.equipmentList[1]);
    }
}
