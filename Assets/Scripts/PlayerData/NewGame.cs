using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class NewGame : MonoBehaviour
{
    public TMP_InputField textBox;
    public GameObject exitTransition;
    public GameObject confirmNewGame;
    public GameObject newPlayerInput;
    public Button startButton;
    
    public bool confirmNG = false;

    private void Start() {
        Button btn = startButton.GetComponent<Button>();
		btn.onClick.AddListener(delegate{onClickCheck(SaveData.dataInstance.returnSaveExist());});
    }

    void onClickCheck(bool saveExist) {
        if (saveExist) {
            confirmNewGame.SetActive(true);
        } else {
            newPlayerInput.SetActive(true);
            //reset playerprefs
            // PlayerPrefs.DeleteAll();
            // StartCoroutine(gameStart());
            // SaveData.dataInstance.save2Json();
        }
    }

    public void startTheGame() {
        StartCoroutine(gameStart());
    }

    IEnumerator gameStart() {
        PlayerPrefs.SetString("name", textBox.text);
        yield return new WaitForSeconds(1f);
        exitTransition.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        SaveData.dataInstance.startNewGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
