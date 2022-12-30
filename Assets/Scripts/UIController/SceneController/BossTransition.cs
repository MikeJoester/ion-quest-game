using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTransition : MonoBehaviour
{
    public Collider2D wall;
    public GameObject bossInfo;
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            wall.isTrigger = false;
            bossInfo.SetActive(true);
        }
    }
}
