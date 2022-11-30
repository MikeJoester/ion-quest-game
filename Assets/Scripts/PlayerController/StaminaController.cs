using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    private Slider staminaBar;
    private float maxStamina = 150f;
    private float currentStamina;

    private WaitForSeconds delay = new WaitForSeconds(0.1f);
    private Coroutine regen;
    
    public static StaminaController instance;

    void Awake() {
        instance = this;
    }
    
    void Start() {
        staminaBar = GetComponent<Slider>();
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    public void UseStamina(float amount) {
        if (currentStamina - amount >= 0) {
            currentStamina -=  amount;
            staminaBar.value = currentStamina;
            
            if(regen != null) {
                StopCoroutine(regen);
            }

            regen = StartCoroutine(StaminaRegen());
        }
        else {
            PlayerController.isDashing = false;
        }
    }

    IEnumerator StaminaRegen(){
        yield return new WaitForSeconds(1f);

        while (currentStamina < maxStamina) {
            currentStamina += maxStamina / 50;
            staminaBar.value = currentStamina;
            yield return delay;
        }
        regen = null;
    }
}