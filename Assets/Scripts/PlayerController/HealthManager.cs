using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    public int maxHealth;
    int health;

    public event Action DamageTaken;
    public event Action addedHearts;

    public int Health {
        get { return health; }
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {
        health = maxHealth;
    }

    public void TakeDmg() {
        if (health <= 0) {
            Debug.Log("YOU DIED!");
            return;
        }
        health -= 1;

        if (DamageTaken != null) {
            DamageTaken();
        }
    }

    public void Healing() {
        if (health >= maxHealth) {
            return;
        }

        health += 1;
        if (DamageTaken != null) {
            DamageTaken();
        }
    }

    public void UpgradeHealth() {
        maxHealth++;
        health = maxHealth;
        
        if (addedHearts!= null) {
            addedHearts();
        }
    }
}
