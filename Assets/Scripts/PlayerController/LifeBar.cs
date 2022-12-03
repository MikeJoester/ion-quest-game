using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField] GameObject heart;
    [SerializeField] List<Image> heartList;

    HealthManager healthManager;

    void Start() {
        healthManager = HealthManager.instance;
        healthManager.DamageTaken += updateHearts;
        healthManager.addedHearts += addHearts;
        for (int i = 0; i < healthManager.maxHealth; i++) {
            GameObject hp = Instantiate(heart, this.transform);
            heartList.Add(hp.GetComponent<Image>());
        }
    }

    void updateHearts() {
        int heartFill = healthManager.Health;
        foreach(Image i in heartList) {
            i.fillAmount = heartFill;
            heartFill -= 1;
        }
    }

    void addHearts() {
        foreach(Image i in heartList) {
            Destroy(i.gameObject);
        }
        heartList.Clear();
        for (int i = 0; i < healthManager.maxHealth; i++) {
            GameObject hp = Instantiate(heart, this.transform);
            heartList.Add(hp.GetComponent<Image>());
        }
    }
}
