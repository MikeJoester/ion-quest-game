using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject emptySlot;
    // [SerializeField] List<Image> heartList;

    Inventory inventory;

    void Start() {
        inventory = Inventory.invenInstance;
        inventory.addedSlot += addSlot;
        for (int i = 0; i < inventory.maxSlots; i++) {
            GameObject slots = Instantiate(emptySlot, this.transform);
            // heartList.Add(hp.GetComponent<Image>());
        }
    }

    void addSlot() {
        // foreach(Image i in heartList) {
        //     Destroy(i.gameObject);
        // }
        // heartList.Clear();
        for (int i = 0; i < inventory.maxSlots; i++) {
            GameObject slots= Instantiate(emptySlot, this.transform);
            // heartList.Add(hp.GetComponent<Image>());
        }
    }
}
