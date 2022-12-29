using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    public int maxHealth;
    private PlayerController player;
    int healingAmount = 1;
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
        player = FindObjectOfType<PlayerController>();
    }

    public void TakeDmg() {
        health -= 1;
        if (health <= 0) {
            player.setInteract = true;
            StartCoroutine(player.PlayerDead());
            //return;
        }

        if (DamageTaken != null) {
            DamageTaken();
        }
    }

    public void Healing() {
        if (health >= maxHealth) {
            return;
        }

        health += healingAmount;
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
