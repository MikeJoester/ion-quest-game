using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    private Animator anim;
    private Inventory inven;
    public Item item;
    private bool collided = false;
    private bool isOpened = false;

    [SerializeField] GameObject spreadEffect;
    // [SerializeField] GameObject itemButton;

    void Start() {
        anim = GetComponent<Animator>();
        inven = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    void Update() {
        if (!isOpened) {
            if ((collided) && (Input.GetKeyDown("e"))) {
            anim.SetBool("isOpen", true);
            // SetSlot();
            StartCoroutine(SpawnSpread());
            isOpened = true;
            }
        }  
    }

    IEnumerator SpawnSpread(){
        yield return new WaitForSeconds(0.5f);
        spreadEffect.SetActive(true);
        bool pickedUp = Inventory.invenInstance.Add(item);
        if (pickedUp) {
            Debug.Log($"Obtained {item.name}");
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if((other.gameObject.tag == "Player")) {
            collided = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if((other.gameObject.tag == "Player")) {
            collided = false;
        }
    }

    // void SetSlot() {
    //     for (int i = 0; i < inven.maxSlots; i++) {
    //         if (inven.isAvailable[i] == true) {
    //             inven.isAvailable[i] = false;
    //             itemButton.SetActive(true);

    //             GameObject newButton = Instantiate(itemButton, inven.itemSlots[i].transform, false);
    //             newButton.transform.localPosition = Vector3.zero;
    //             break;
    //         }
    //     }
    // }
}
