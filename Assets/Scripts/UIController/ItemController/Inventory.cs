using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public bool[] isAvailable;
    public GameObject[] itemSlots;
    [SerializeField] GameObject emptySlot;
    public static Inventory invenInstance;

    public int maxSlots = 9;
    int itemSlot;

    public event Action addedSlot;

    public int Slot {
        get { return itemSlot; }
    }

    void Awake() {
        if (invenInstance == null) {
            invenInstance = this;
        }
    }

    void Start() {
        itemSlot = maxSlots;
        isAvailable = new bool[maxSlots];
        itemSlots = new GameObject[maxSlots];
        for (int i = 0; i < maxSlots; i++) {
            // isAvailable.Add(true);
            // itemSlots.Add(emptySlot.GetComponent<Image>());
            isAvailable[i] = true;
            itemSlots[i] = emptySlot;
        }
    }

    public void UpgradeHealth() {
        maxSlots++;
        itemSlot = maxSlots;
        
        if (addedSlot!= null) {
            addedSlot();
        }
    }
}
