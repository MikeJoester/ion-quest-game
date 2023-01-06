using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {
    public static EquipmentManager instance;
    public delegate void OnEquipmentChanged (Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    Equipment[] currentEquipment;
    Inventory inventory;

    private void Awake() {
        instance = this;
    }

    public Equipment[] equipmentList {
        get { return currentEquipment; }
        set { currentEquipment = value; }
    }

    void Start() {
        inventory = Inventory.invenInstance;

        int slotNums = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[slotNums];
    }

    public void Equip(Equipment newItem) {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null) {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        if (onEquipmentChanged != null) {
            onEquipmentChanged.Invoke(null, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
    }

    public void unEquip(int slotIndex) {
        if (currentEquipment[slotIndex] != null) {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;
            PlayerController.playerInstance.getAtk = 1;
        }
    }

    public void unEquipAll() {
        for (int i = 0; i < currentEquipment.Length; i++) {
            unEquip(i);
        }
    }

}