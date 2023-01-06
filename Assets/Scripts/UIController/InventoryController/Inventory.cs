using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public int storageSpace = 15;

    public static Inventory invenInstance;

    void Awake() {
        if (invenInstance == null) {
            invenInstance = this;
        }
    }

    // public List<Item> itemList = new List<Item>();
    public List<Item> itemList;
    public List<Item> gsItemList {
        get {return itemList;}
        set {itemList = value;}
    }

    public bool Add(Item i) {
        if (!i.isDefaultItem) {
            if (itemList.Count >= storageSpace) {
                Debug.Log("Inventory full!");
                return false;
            }

            itemList.Add(i);

            if (onItemChangedCallback != null) { 
                onItemChangedCallback.Invoke();
            }
        }

        return true;
    }

    public void Remove(Item i) {
        itemList.Remove(i);

        if (onItemChangedCallback != null) { 
                onItemChangedCallback.Invoke();
            }
    }
}
