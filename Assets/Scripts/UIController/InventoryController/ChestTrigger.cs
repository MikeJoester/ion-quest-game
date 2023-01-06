using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    private Animator anim;
    private Inventory inven;
    public Item item;
    private bool collided = false;
    public bool isOpened = false;

    [SerializeField] GameObject spreadEffect;
    [SerializeField] GameObject interactArrow;
    [SerializeField] GameObject dialogueBox;
    void Start() {
        anim = GetComponent<Animator>();
        inven = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        dialogueBox.SetActive(false);
        interactArrow.SetActive(false);
    }

    void Update() {
        if (isOpened) {
            anim.SetBool("isOpen", true);
        }  else {
            if ((collided) && (Input.GetKeyDown("f"))) {
                anim.SetBool("isOpen", true);
                StartCoroutine(SpawnSpread());
                FindObjectOfType<AudioController>().playClip("Obtain");
                isOpened = true;
                interactArrow.SetActive(false);
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
            interactArrow.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if((other.gameObject.tag == "Player")) {
            collided = false;
            interactArrow.SetActive(false);
        }
    }
}
