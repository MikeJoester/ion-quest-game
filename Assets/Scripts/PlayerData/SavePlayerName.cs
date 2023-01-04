using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SavePlayerName : MonoBehaviour
{
    public TMP_InputField textBox;
    public GameObject exitTransition;

    public void onClickSave() {
        PlayerPrefs.DeleteAll(); //reset playerprefs

        StartCoroutine(gameStart());
    }

    IEnumerator gameStart() {
        PlayerPrefs.SetString("name", textBox.text);
        yield return new WaitForSeconds(1f);
        exitTransition.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
