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
    [SerializeField] GameObject dialogueBox;

    void Start() {
        anim = GetComponent<Animator>();
        inven = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        dialogueBox.SetActive(false);
    }

    void Update() {
        if (!isOpened) {
            if ((collided) && (Input.GetKeyDown("f"))) {
            anim.SetBool("isOpen", true);
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
            // Debug.Log($"Obtained {item.name}");
            dialogueBox.SetActive(true);
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
}
