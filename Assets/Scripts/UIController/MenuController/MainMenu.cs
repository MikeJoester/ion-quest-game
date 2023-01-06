using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Alert;
    public static MainMenu menuInstance;

    private void Awake() {
        if (menuInstance == null) {
            menuInstance = this;
        }
    }

    public void Quit() {
        Application.Quit();
    }

    public void PlayNew() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackToStart() {
        SceneManager.LoadScene(0);
    }

    public void LoadProfile() {
        
    }

    public void setAlert(bool isOn) {
        if (Alert != null) {
            Alert.SetActive(isOn);
        }
    }
}
