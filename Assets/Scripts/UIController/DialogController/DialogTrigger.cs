using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    private bool triggerEntered = false;
    public GameObject DialogueScreen;

    void Update() {
        if (Input.GetKeyDown(KeyCode.F) && triggerEntered == true) {
            DialogueScreen.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            triggerEntered = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        triggerEntered = false;
    }
}
