using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    private Slider HPbar;
    public EnemyController enemyData;
    void Start() {
        HPbar = GetComponent<Slider>();
        HPbar.maxValue = enemyData.enemyHP;
    }

    // Update is called once per frame
    void Update() {
        HPbar.value = enemyData.enemyHP;
    }
}