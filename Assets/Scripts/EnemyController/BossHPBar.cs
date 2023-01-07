using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    private Slider HPbar;
    private BossTransition bossBorder;
    public EnemyController enemyData;
    public GameObject chestObj;
    public Collider2D bossBorderBox;
    
    void Start() {
        HPbar = GetComponent<Slider>();
        bossBorder = FindObjectOfType<BossTransition>();
        // bossBorderBox = GetComponent<Collider2D>();
        HPbar.maxValue = enemyData.enemyHP;
    }

    // Update is called once per frame
    void Update() {
        HPbar.value = enemyData.enemyHP;
        if (HPbar.value <= 0) {
            setActiveChest();
        }
    }

    void setActiveChest() {
        bossBorder.bossWall.SetActive(false);
        bossBorder.bossInfo.SetActive(false);
        bossBorderBox.enabled = false;
        chestObj.transform.position = enemyData.transform.position;
        chestObj.SetActive(true);
    }
}
