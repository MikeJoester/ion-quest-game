using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    private Animator anim;
    private bool collided;

    void Start()
    {
        anim = GetComponent<Animator>(); 
        collided = false;  
    }

    void Update() {
        if ((collided) && (Input.GetKeyDown("e"))) {
            anim.SetBool("isOpen", true);
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
