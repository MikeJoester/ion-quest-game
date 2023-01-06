using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {
    public static EquipmentManager instance;
    private void Awake() {
        instance = this;
    }

    Equipment[] currentEquipment;

    void Start() {
        int slotNums = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[slotNums];
    }

    public void Equip(Equipment newItem) {
        int slotIndex = (int)newItem.equipSlot;

        currentEquipment[slotIndex] = newItem;
    }
}