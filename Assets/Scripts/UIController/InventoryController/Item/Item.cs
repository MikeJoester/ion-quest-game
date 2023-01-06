using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item", order = 0)]

public class Item : ScriptableObject {
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use() {
        // Debug.Log($"Using {name}");
        switch (name) {
            case "Potion":
                FindObjectOfType<AudioController>().playClip("Heal");
                HealthManager.instance.Healing(2);
                break;
            case "BoostPotion":
                FindObjectOfType<AudioController>().playClip("Stamina");
                StaminaController.instance.setCurrentStamina = 150f;
                break;
        }
    }

    public void removeFromInven() {
        Inventory.invenInstance.Remove(this);
    }
}