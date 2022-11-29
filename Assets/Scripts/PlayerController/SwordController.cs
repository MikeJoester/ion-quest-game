using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    private Collider2D swordCollider;

    void Start() {
        swordCollider = GetComponent<Collider2D>();
        swordCollider.enabled = false;
    }

    public void Attack() {
        swordCollider.enabled = true;
    }

    public void stopAttack() {
        swordCollider.enabled = false;
    }
}

